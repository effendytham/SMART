using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.Services
{
    public interface IElasticSearch
    {
        object Search(string searchTerm, int size = 25, int page = 0, Dictionary<string,object> arguments = null);
    }
}
