﻿using Cafe.Infrastructure.IoC;

namespace Cafe.WebAPI.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {//servis bağımlılıklarımızın eklediğimiz yer
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
