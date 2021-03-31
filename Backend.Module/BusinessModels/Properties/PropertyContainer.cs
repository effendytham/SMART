using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.BusinessModels
{
    public class PropertyContainer
    {
        public string id
        {
            get
            {
                return property.propertyId;
            }
        }
        public PropertyContainer()
        {
            property = new Property();
        }
        public Property property { get; set; }
    }
}
