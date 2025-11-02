using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.MultiTenancy
{
    public class TenantSetting
    {
        public TenantConfig DefaultConfig { get; set; }
        public List<Tenant> Tenants { get; set; }
    }
}
