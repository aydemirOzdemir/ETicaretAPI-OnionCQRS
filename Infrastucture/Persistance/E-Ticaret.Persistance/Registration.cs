using E_Ticaret.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Persistance;

public static class Registration
{
    public static IServiceCollection AddPersistanceRegister(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ETicaretDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
}
