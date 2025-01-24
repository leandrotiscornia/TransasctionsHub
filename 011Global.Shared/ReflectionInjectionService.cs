using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared;

public static class ReflectionInjectionService
{
    public static IServiceCollection LoadInterfacesTransient<T>(this IServiceCollection services) where T : class
    {
        var interfaceImplementors = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => typeof(T).IsAssignableFrom(p) && p.IsClass);
        foreach (var interfaceImplementor in interfaceImplementors)
        {
            services.AddTransient(typeof(T), interfaceImplementor);
        }

        return services;
    }

    public static IServiceCollection LoadInterfacesSingleton<T>(this IServiceCollection services) where T : class
    {
        var interfaceImplementors = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => typeof(T).IsAssignableFrom(p) && p.IsClass);
        foreach (var interfaceImplementor in interfaceImplementors)
        {
            services.AddSingleton(typeof(T), interfaceImplementor);
        }

        return services;
    }
}
