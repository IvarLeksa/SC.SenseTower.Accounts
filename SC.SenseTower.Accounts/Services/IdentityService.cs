using AutoMapper;
using Flurl;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SC.SenseTower.Accounts.Constants;
using SC.SenseTower.Accounts.Dto.Identity;
using SC.SenseTower.Accounts.Dto.Server;
using SC.SenseTower.Accounts.Settings;
using SC.SenseTower.Common.Exceptions;
using SC.SenseTower.Common.Extensions;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SC.SenseTower.Accounts.Services
{
    public class IdentityService
    {
        private const string OPENID_CONFIGURATION_ENDPOINT = ".well-known/openid-configuration";

        private readonly HttpClient httpClient;
        private readonly IdentityServerSettings settings;
        private readonly IMemoryCache cache;
        private readonly IMapper mapper;

        public IdentityService(
            HttpClient httpClient,
            IOptions<IdentityServerSettings> options,
            IMemoryCache cache,
            IMapper mapper)
        {
            this.cache = cache;
            this.mapper = mapper;
            settings = options.Value;
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(settings.BaseUrl);
        }

        public async Task<IdentityResult> Register(string login, string email, string password, CancellationToken cancellationToken)
        {
            var result = await Post<bool>(null, settings.RegisterUrl, new { login, email, password }, cancellationToken).ConfigureAwait(false);
            if (!result)
                return IdentityResult.Failed(new IdentityError { Code = "RegistrationError", Description = "Ошибка регистрации" });
            return IdentityResult.Success;
        }

        public async Task<TokenResponseDto?> Logon(string login, string password, CancellationToken cancellationToken)
        {
            var serverConfig = await GetOpenIdConfiguration(cancellationToken).ConfigureAwait(false);
            var response = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = serverConfig.TokenEndpoint,
                ClientId = settings.ClientId,
                UserName = login,
                Password = password
            });
            if (response.IsError)
                throw new Exception(response.Error);

            var result = mapper.Map<TokenResponseDto>(response);
            return result;
        }

        public async Task<UserInfoResponseDto?> GetIdentityInfo(string token, CancellationToken cancellationToken)
        {
            var serverConfig = await GetOpenIdConfiguration(cancellationToken).ConfigureAwait(false);
            var response = await httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = serverConfig.UserinfoEndpoint,
                Token = token
            });
            if (response.IsError)
                throw new ScException(response.Error);

            var result = JsonSerializer.Deserialize<UserInfoResponseDto>(response.Raw);
            result.AccessToken = token;
            return result;
        }

        public async Task<bool> IsLoginFree(string login, CancellationToken cancellationToken)
        {
            try
            {
                var result = await Get<bool>(null, settings.IsLoginFreeUrl, new { login }, cancellationToken).ConfigureAwait(false);
                return result;
            }
            catch (ScException)
            {
                return false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> IsEmailFree(string email, CancellationToken cancellationToken)
        {
            try
            { 
                var result = await Get<bool>(null, settings.IsEmailFreeUrl, new { email }, cancellationToken).ConfigureAwait(false);
                return result;
            }
            catch (ScException)
            {
                return false;
            }
            catch
            {
                throw;
            }
        }

        #region Privates

        private async Task<OpenIdConfigurationDto> GetOpenIdConfiguration(CancellationToken cancellationToken)
        {
            var result = await cache.GetOrCreateAsync(CacheKeys.SERVER_CONFIG, async e =>
            {
                e.SlidingExpiration = TimeSpan.FromMinutes(10);
                return await Get<OpenIdConfigurationDto>(null, OPENID_CONFIGURATION_ENDPOINT, null, cancellationToken).ConfigureAwait(false);
            });
            if (result == null)
                throw new Exception("Не удалось прочитать конфигурацию OpenID");
            return result;
        }

        private async Task<T?> Get<T>(string? token, string url, object? data, CancellationToken cancellationToken)
        {
            if (data != null)
                url = url.SetQueryParams(data);
            url = url.SetQueryParam("_", DateTime.Now.Ticks);
            httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(token) ? null : new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            return await GetResponseContent<T>(response);
        }

        private async Task<T?> Post<T>(string? token, string url, object? data, CancellationToken cancellationToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(token) ? null : new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var content = data != null ? new FormUrlEncodedContent(data.ToDictionary()) : null;
            var response = await httpClient.PostAsync(url, content, cancellationToken).ConfigureAwait(false);
            return await GetResponseContent<T>(response);
        }

        private async Task<T?> GetResponseContent<T>(HttpResponseMessage? response)
        {
            if (response == null)
                throw new Exception("Не получен ответ с удаленного ресурса");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new ScException(text);
            }

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                var result = JsonSerializer.Deserialize<T?>(json);
                return result;
            }
            catch
            {
                return default(T);
            }
        }

        #endregion
    }
}
