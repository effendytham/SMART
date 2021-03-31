using Backend.Module.BusinessModels;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Module.Services.ElasticSearch.SearchImplementation
{
    public class PropertyAndManagementSearch : IElasticSearch
    {
        public PropertyAndManagementSearch(IElasticService elasticService)
        {
            _elasticService = elasticService;
        }

        private IElasticService _elasticService;

        public object Search(string searchTerm, int size = 25, int page = 0, Dictionary<string, object> arguments = null)
        {

            var propertyResults = SearchingProperties(searchTerm, size, page, arguments);
            var managementResults = SearchingManagement(searchTerm, size, page, arguments);
            
            //Merging property result and management result while sort them based on score
            List<Model> models = (propertyResults.Hits.Select(x => new Model()
            {
                id = x.Source.property.propertyId,
                name = x.Source.property.name,
                formerName = x.Source.property.formerName,
                type = "property",
                score = x.Score,
                market = x.Source.property.market
            }).ToList());

            models.AddRange(managementResults.Hits.Select(x => new Model()
            {
                id = x.Source.mgmt.mgmtID,
                name = x.Source.mgmt.name,
                formerName = string.Empty,
                type = "management",
                score = x.Score,
                market = x.Source.mgmt.market
            }).ToList());

            return models.OrderByDescending(x => x.score);
        }

        private ISearchResponse<PropertyContainer> SearchingProperties(string term, int size, int page, Dictionary<string, object> arguments)
        {

            var properties = _elasticService.Search<PropertyContainer>(x =>
                x.Index(Indices.Index("properties"))
                .Size(size)
                .From(page)
                .Query(q =>
                    q.Bool(
                        b1 => b1.Should(
                                s1 => s1.Match(m1 => m1.Field("property.name").Query(term).Fuzziness(Fuzziness.EditDistance(2)).Operator(Operator.And).Analyzer("stop")),
                                s1 => s1.Match(m1 => m1.Field("property.formerName").Query(term).Fuzziness(Fuzziness.EditDistance(2)).Operator(Operator.And).Analyzer("stop"))
                            ).Must(
                                s1 =>
                                {
                                    if (arguments["market"] != null && arguments["market"].ToString().Trim() != string.Empty)
                                    {
                                        string[] markets = arguments["market"].ToString().Trim().Split(",");
                                        List<QueryContainer> queries = new List<QueryContainer>();
                                        foreach (string market in markets)
                                        {
                                            queries.Add(new MatchQuery()
                                            {
                                                Field = "property.market",
                                                Query = market
                                            });
                                        }
                                        return new QueryContainerDescriptor<PropertyContainer>().Bool(
                                                d => d.Should(queries.ToArray()));
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                })
                            )
                        )
                );
                 
            return properties;
        } 

        private ISearchResponse<ManagementContainer> SearchingManagement(string term, int size, int page, Dictionary<string, object> arguments)
        {
            var managements = _elasticService.Search<ManagementContainer>(x =>
                 x.Index(Indices.Index("managements"))
                 .Size(size)
                 .From(page)
                 .Query(q =>
                     q.Bool(
                         b1 => b1.Should(
                                 s1 => s1.Match(m1 => m1.Field("mgmt.name").Query(term).Fuzziness(Fuzziness.EditDistance(2)).Operator(Operator.And).Analyzer("stop"))
                             ).Must(
                                 s1 =>
                                 {
                                     if (arguments["market"] != null && arguments["market"].ToString().Trim() != string.Empty)
                                     {
                                         string[] markets = arguments["market"].ToString().Trim().Split(",");
                                         List<QueryContainer> queries = new List<QueryContainer>();
                                         foreach (string market in markets)
                                         {
                                             queries.Add(new MatchQuery()
                                             {
                                                 Field = "mgmt.market",
                                                 Query = market
                                             });
                                         }
                                         return new QueryContainerDescriptor<ManagementContainer>().Bool(
                                                 d => d.Should(queries.ToArray()));
                                     }
                                     else
                                     {
                                         return null;
                                     }
                                 })
                             )
                         )
                 );

            return managements;
        }

        public class Model
        {
            public string id { get; set; }
            public string name { get; set; }
            public string formerName { get; set; }
            public string type { get; set; }
            public string market { get; set; }
            public object score { get; set; }
        }

    }
}
