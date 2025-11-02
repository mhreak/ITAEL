using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfraStructure.MultiTenancy
{
    public class Tenant
    {
        public string Name { get; set; }
        public string TID { get; set; }
        public string ConnectionString { get; set; }
    }
}
