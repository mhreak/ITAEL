using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IWalletService
    {
        int Add(WalletViewModel uiModel);

        bool Edit(WalletViewModel uiModel);

        bool Delete(int id);

        WalletViewModel Get(int id);

        IList<WalletViewModel> GetAll();

        public IList<WalletViewModel> GetAllFiltered(
            string filterWalletName,
            string filterActive,
            string filterInsertDateFrom, string filterInsertDateTo,
            int currentPage, int pageSize, out int totalRecord);

        bool IsDuplicateByWalletName(int? walletId, string walletName);
    }
}
