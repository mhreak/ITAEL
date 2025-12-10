using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shodamad.Model.OnlinePayment
{
    public class ZarinpalVerifyParameters
    {
        public string amount { set; get; }
        public string merchant_id { set; get; }
        public string authority { set; get; }
    }
}
