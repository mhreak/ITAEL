using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IPaymentTypeService
    {
        PaymentTypeViewModel Get(int id);

        IList<PaymentTypeViewModel> GetAll();
    }
}
