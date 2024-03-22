using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Behaviours;
using E_Ticaret.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        services.AddRulesFromAssemblyContaing(Assembly.GetExecutingAssembly(),typeof(BaseRules));  services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBahevior<,>));
        return services;
    }
    private static IServiceCollection AddRulesFromAssemblyContaing(this IServiceCollection services,Assembly assembly,Type type)
    {
        var types= assembly.GetTypes().Where(t=>t.IsSubclassOf(type)&& type != t).ToList();
        foreach (var t in types) services.AddTransient(t);
        return services;
    }
}
