using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Backend.Module.Services
{
    public class ElasticService : IElasticService
    {
        private IAppSettingsProvider _appSettingsProvider;
        private AppSettings _appSettings;
        public ElasticService(IAppSettingsProvider appSettingsProvider)
        {
            _appSettingsProvider = appSettingsProvider;
            _appSettings = _appSettingsProvider.GetSettings();
        }

        public ServiceResponse IndexingDocuments<T>(string index, List<T> objects) where T : class
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var settings = new ConnectionSettings(new Uri(_appSettings.ElasticSearch.Host));
                var client = new ElasticClient(settings);
                var bulkResponse = client.Bulk(b => b
                    .Index(index)
                    .IndexMany(objects));
                response.Success = !bulkResponse.Errors;
                response.Errors = bulkResponse.ItemsWithErrors
                                                  .Select(x => string.Format("Document id : {0} encounter an error", x.Id.ToString()))
                                                  .ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;

        }

        public ServiceResponse IndexingDocument<T>(string index, T obj) where T : class
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var settings = new ConnectionSettings(new Uri(_appSettings.ElasticSearch.Host));
                var client = new ElasticClient(settings);
                var indexResponse = client.Index<T>(obj, i => i.Index(index));
                response.Success = indexResponse.IsValid;
                response.Errors.Add("Document encounter an error");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class
        {
            try
            {
                var settings = new ConnectionSettings(new Uri(_appSettings.ElasticSearch.Host));
                var client = new ElasticClient(settings);
                return client.Search<T>(selector);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
