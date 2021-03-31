using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.BusinessModels
{
    public class ManagementContainer
    {
        public ManagementContainer()
        {
            mgmt = new Management();
        }
        public string id
        {
            get
            {
                return mgmt.mgmtID;
            }
        }
        public Management mgmt { get; set; }
    }
}
