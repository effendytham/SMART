using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.Services
{
    public class QueryResponse<T> : ServiceResponse where T : class
    {
        public T Data { get; set; }
    }
}
