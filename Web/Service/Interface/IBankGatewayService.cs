using System.Collections.Generic;

using Web.Model;

namespace Web.Service.Interface
{
    public interface IBankGatewayService
    {
        int Add(BankGatewayViewModel uiModel);
        bool Edit(BankGatewayViewModel uiModel);
        IList<BankGatewayViewModel> GetAll(bool? active);
        BankGatewayViewModel Get(int id);
    }
}
