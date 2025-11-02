using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfraStructure.MultiTenancy
{
    public class TenantConfig
    {
        public string DBProvider { get; set; }
        public string ConnectionString { get; set; }
    }
}
