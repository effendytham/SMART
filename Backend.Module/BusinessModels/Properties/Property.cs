using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.BusinessModels
{
    public class Property
    {
        public string propertyId { get; set; }
        public string name { get; set; }
        public string formerName { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string market { get; set; }
        public string state { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
