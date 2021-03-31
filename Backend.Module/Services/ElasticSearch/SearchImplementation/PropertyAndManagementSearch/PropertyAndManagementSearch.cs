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
            List<Func<QueryContainerDescriptor<PropertyContainer>, QueryContainer>> propertyMusts = new List<Func<QueryContainerDescriptor<PropertyContainer>, QueryContainer>>();
            propertyMusts.Add(x => x.Match(m =>
                                        m.Field("property.name").Query(term).Fuzziness(Fuzziness.EditDistance(2))) || 
                                    x.Match(m => 
                                        m.Field("property.formerName").Query(term).Fuzziness(Fuzziness.EditDistance(2))));

            if (arguments.ContainsKey("market") && arguments["market"] != null)
            {
                propertyMusts.Add(x => x.Term(t => t.property.market, arguments["market"].ToString().ToLower()));
            }

            var properties = _elasticService.Search<PropertyContainer>(x =>
                x.Index(Indices.Index("properties"))
                .Size(size)
                .From(page)
                .Query(q => q.Bool(b => b.Must(propertyMusts))));
            return properties;
        } 

        private ISearchResponse<ManagementContainer> SearchingManagement(string term, int size, int page, Dictionary<string, object> arguments)
        {
            List<Func<QueryContainerDescriptor<ManagementContainer>, QueryContainer>> managementMusts = new List<Func<QueryContainerDescriptor<ManagementContainer>, QueryContainer>>();
            managementMusts.Add(x => x.Match(m =>
                                m.Field("mgmt.name").Query(term)));

            if (arguments.ContainsKey("market") && arguments["market"] != null)
            {
                managementMusts.Add(x => x.Term(t => t.mgmt.market, arguments["market"].ToString().ToLower()));
            }


            var managements = _elasticService.Search<ManagementContainer>(x =>
                x.Index(Indices.Index("managements"))
                .Size(size)
                .From(page)
                .Query(q => q.Bool(b => b.Must(managementMusts))));
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
