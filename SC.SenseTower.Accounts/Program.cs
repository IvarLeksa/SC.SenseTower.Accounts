using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SC.SenseTower.Accounts.Dto;
using SC.SenseTower.Accounts.Extensions;
using SC.SenseTower.Accounts.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<IdentityServerSettings>(configuration.GetSection(nameof(IdentityServerSettings)));

var isConfig = configuration.GetSection(nameof(IS4Configuration)).Get<IS4Configuration>();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.Authority = isConfig.Authority;
        o.Audience = isConfig.Audience;
    });

services.AddMediatR(Assembly.GetCallingAssembly());
services.AddApplicationServices();
services.AddValidators();
services.AddHttpClients(configuration);
services.AddAutoMapper(new[]
{
    typeof(MappingProfile)
});

services.AddMemoryCache();

// Add services to the container.
services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization();

app.Run();
