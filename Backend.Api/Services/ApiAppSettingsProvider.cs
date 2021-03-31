using Backend.Module.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Api.Services
{
    public class ApiAppSettingsProvider : IAppSettingsProvider
    {
        public ApiAppSettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private IConfiguration _configuration;

        public AppSettings GetSettings()
        {
            try
            {
                AppSettings settings = new AppSettings();
                settings.ElasticSearch.Host = _configuration.GetSection("AppSettings:ElasticSearch:Host").Value;

                int elasticPort;
                if (int.TryParse(_configuration.GetSection("AppSettings:ElasticSearch:Port").Value, out elasticPort))
                {
                    settings.ElasticSearch.Port = elasticPort;
                }
                else
                {
                    throw new ArgumentException("Elastic host's port configuration is not correct");
                }
                return settings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
