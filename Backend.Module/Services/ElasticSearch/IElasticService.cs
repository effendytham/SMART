using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.Services
{
    public interface IElasticService
    {
        ElasticClient GetClient();
        ServiceResponse IndexingDocuments<T>(string index, List<T> objects) where T : class;
        ServiceResponse IndexingDocument<T>(string index, T obj) where T : class;
        ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class;
    }
}
