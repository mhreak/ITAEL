using Web.Model;
using System.Collections.Generic;

namespace Web.Service.Interface
{
    public interface IWallet_Collaborator_CommissionRule_Service
    {
        bool Add(Wallet_Collaborator_CommissionRule_ViewModel uiModel);

        bool Edit(Wallet_Collaborator_CommissionRule_ViewModel uiModel);

        bool Delete(int walletId, int collaboratorId, int commissionRuleId);

        bool IsDuplicate(int walletId, int collaboratorId, int commissionRuleId);

        Wallet_Collaborator_CommissionRule_ViewModel Get(int walletId, int collaboratorId, int commissionRuleId);

        IList<Wallet_Collaborator_CommissionRule_ViewModel> GetAllByWalletId(int walletId);

        IList<Wallet_Collaborator_CommissionRule_ViewModel> GetAllByCollaboratorId(int collaboratorId);

        IList<Wallet_Collaborator_CommissionRule_ViewModel> GetAllCommissionRuleId(int commissionRuleId);
    }
}
