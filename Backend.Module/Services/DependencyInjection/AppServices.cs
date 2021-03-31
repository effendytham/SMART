using Backend.Module.Services.ElasticSearch;
using Backend.Module.Services.ElasticSearch.SearchImplementation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Backend.Module.Services
{
    public static class AppServices
    {
        public static IServiceCollection AddSmartServices(this IServiceCollection services, Type appSettingsProviderService){

            services.AddTransient(typeof(IAppSettingsProvider), appSettingsProviderService);
            services.AddTransient<IElasticSearch, PropertyAndManagementSearch>();
            services.AddTransient<IElasticService, ElasticService>();
            services.AddTransient<IElasticSeed, ElasticSeed>();
            return services;
        }
    }
}