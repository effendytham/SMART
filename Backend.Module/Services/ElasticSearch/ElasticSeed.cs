using Backend.Module.BusinessModels;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.Services
{
    public class ElasticSeed : IElasticSeed
    {
        public ElasticSeed(IElasticService elasticService)
        {
            _elasticService = elasticService;
            _elasticClient = _elasticService.GetClient();
        }
        private IElasticService _elasticService;
        private ElasticClient _elasticClient;

        public ServiceResponse Seed()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                IndexMappingGeneration();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                return response;
            }
        }

        private void IndexMappingGeneration()
        {
            if (!(_elasticClient.Indices.Exists("properties").Exists))
            {
                var response = _elasticClient.Indices.Create("properties", c => c
                    .Map<PropertyContainer>(m => m.AutoMap()
                                                    .Properties(ps => ps
                                                          .Keyword(s => s
                                                                .Name(n => n
                                                                    .property.market
                                                                    )
                                                                )
                                                          )));
            }

            if (!(_elasticClient.Indices.Exists("managements").Exists))
            {
                var response = _elasticClient.Indices.Create("managements", c => c
                    .Map<ManagementContainer>(m => m.AutoMap()
                                                    .Properties(ps => ps
                                                          .Keyword(s => s
                                                                .Name(n => n
                                                                    .mgmt.market
                                                                    )
                                                                )
                                                          )));
            }
        }
    }
}
