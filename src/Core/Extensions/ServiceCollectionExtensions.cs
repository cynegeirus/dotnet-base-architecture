﻿using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,
        CoreModule[] modules)
    {
        foreach (var module in modules) module.Load(services);

        return ServiceTool.Create(services);
    }
}