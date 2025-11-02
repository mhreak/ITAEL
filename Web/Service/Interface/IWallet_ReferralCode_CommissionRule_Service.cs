using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IWallet_ReferralCode_CommissionRule_Service
    {
        bool Add(Wallet_ReferralCode_CommissionRule_ViewModel uiModel);

        bool Edit(Wallet_ReferralCode_CommissionRule_ViewModel uiModel);

        bool Delete(int walletId, int referralCodeId, int commissionRuleId);

        bool IsDuplicate(int walletId, int referralCodeId, int commissionRuleId);

        Wallet_ReferralCode_CommissionRule_ViewModel Get(int walletId, int referralCodeId, int commissionRuleId);

        IList<Wallet_ReferralCode_CommissionRule_ViewModel> GetAllByWalletId(int walletId);

        IList<Wallet_ReferralCode_CommissionRule_ViewModel> GetAllByReferralCodeId(int referralCodeId);

        IList<Wallet_ReferralCode_CommissionRule_ViewModel> GetAllCommissionRuleId(int commissionRuleId);
    }
}
