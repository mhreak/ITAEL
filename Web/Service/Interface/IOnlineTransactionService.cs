using System;
using System.Collections.Generic;

using Web.Model;

namespace Web.Service.Interface
{
    public interface IOnlineTransactionService
    {
        long Add(OnlineTransactionViewModel uiModel);
        IList<OnlineTransactionViewModel> GetAllFiltered(string filterApplicantId,
            string filterExamResourceId,
            string filterAmountFrom, string filterAmountTo,
            string filterDateFrom, string filterDateTo, string filterState,
            int currentPage, int pageSize, string sortField, string sortDirection, out int totalRecord);
        IList<OnlineTransactionViewModel> GetAllByApplicantId(int applicantId, short? state);
        //IList<OnlineTransactionViewModel> GetAllByExamResourceId(int examResourceId, short? state);
        //long GetSumByExamResourceId(int examResourceId, short? state);
        OnlineTransactionViewModel Get(long id);
        bool SetReferenceNumber(long transactionId, string refNumber);
        bool SetState(long transactionId, short state, DateTime? verifyDate);
    }
}
