using E_Ticaret.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application;

public static class Registration
{
    public static IServiceCollection AddApplicationRegister(this IServiceCollection services)
    {
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient<ExceptionMiddleware>();
        return services;
    }
}
