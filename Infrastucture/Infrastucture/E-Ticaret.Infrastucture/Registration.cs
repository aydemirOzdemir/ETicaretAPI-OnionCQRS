using E_Ticaret.Application.Interfaces.Tokens;
using E_Ticaret.Infrastucture.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Infrastucture;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegister(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<TokenSetting>(configuration.GetSection("JWT"));
        services.AddTransient<ITokenService, TokenService>();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            opt.SaveToken = true;
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                ValidateLifetime = false,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                ClockSkew=TimeSpan.Zero,

            };

        });
        return services;
    }
}
