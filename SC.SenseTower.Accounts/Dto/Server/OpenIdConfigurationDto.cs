using System.Text.Json.Serialization;

namespace SC.SenseTower.Accounts.Dto.Server
{
    public class OpenIdConfigurationDto
    {
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("jwks_uri")]
        public string JwksUri { get; set; }

        [JsonPropertyName("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; }

        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; }

        [JsonPropertyName("userinfo_endpoint")]
        public string UserinfoEndpoint { get; set; }

        [JsonPropertyName("end_session_endpoint")]
        public string EndSessionEndpoint { get; set; }

        [JsonPropertyName("check_session_iframe")]
        public string CheckSessionIframe { get; set; }

        [JsonPropertyName("revocation_endpoint")]
        public string RevocationEndpoint { get; set; }

        [JsonPropertyName("introspection_endpoint")]
        public string IntrospectionEndpoint { get; set; }

        [JsonPropertyName("device_authorization_endpoint")]
        public string DeviceAuthorizationEndpoint { get; set; }

        [JsonPropertyName("frontchannel_logout_supported")]
        public bool FrontchannelLogoutSupported { get; set; }

        [JsonPropertyName("frontchannel_logout_session_supported")]
        public bool FrontchannelLogoutSessionSupported { get; set; }

        [JsonPropertyName("backchannel_logout_supported")]
        public bool BackchannelLogoutSupported { get; set; }

        [JsonPropertyName("backchannel_logout_session_supported")]
        public bool BackchannelLogoutSessionSupported { get; set; }

        [JsonPropertyName("scopes_supported")]
        public List<string> ScopesSupported { get; set; }

        [JsonPropertyName("claims_supported")]
        public List<string> ClaimsSupported { get; set; }

        [JsonPropertyName("grant_types_supported")]
        public List<string> GrantTypesSupported { get; set; }

        [JsonPropertyName("response_types_supported")]
        public List<string> ResponseTypesSupported { get; set; }

        [JsonPropertyName("response_modes_supported")]
        public List<string> ResponseModesSupported { get; set; }

        [JsonPropertyName("token_endpoint_auth_methods_supported")]
        public List<string> TokenEndpointAuthMethodsSupported { get; set; }

        [JsonPropertyName("id_token_signing_alg_values_supported")]
        public List<string> IdTokenSigningAlgValuesSupported { get; set; }

        [JsonPropertyName("subject_types_supported")]
        public List<string> SubjectTypesSupported { get; set; }

        [JsonPropertyName("code_challenge_methods_supported")]
        public List<string> CodeChallengeMethodsSupported { get; set; }

        [JsonPropertyName("request_parameter_supported")]
        public bool RequestParameterSupported { get; set; }
    }
}
