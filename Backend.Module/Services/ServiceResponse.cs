using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.Services
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
