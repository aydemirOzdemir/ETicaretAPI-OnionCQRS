using AutoMapper;
using E_Ticaret.Application.Interfaces.AutoMapper;
using ETicaret.Mapper.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Mapper;

public static  class Registration
{
    public static IServiceCollection AddAutoMapperRegister(this IServiceCollection services) 
    {
        services.AddSingleton<E_Ticaret.Application.Interfaces.AutoMapper.IMapper, ETicaret.Mapper.AutoMapper.Mapper>();
        return services;
    }
}
