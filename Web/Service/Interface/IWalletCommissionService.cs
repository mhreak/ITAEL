using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IWalletCommissionService
    {
        int Add(WalletCommissionViewModel uiModel);

        bool Edit(WalletCommissionViewModel uiModel);

        bool Delete(int id);

        WalletCommissionViewModel Get(int id);

        public IList<WalletCommissionViewModel> GetAllFiltered(
            string filterWalletId,
            string filterCommissionFrom, string filterCommissionTo,
            string filterInsertDateFrom, string filterInsertDateTo,
            int currentPage, int pageSize, out int totalRecord);
    }
}
