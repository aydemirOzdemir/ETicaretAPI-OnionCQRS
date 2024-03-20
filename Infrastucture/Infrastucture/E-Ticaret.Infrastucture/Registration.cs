using E_Ticaret.Infrastucture.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        return services;
    }
}
