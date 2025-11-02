using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface ICommissionRuleService
    {
        int Add(CommissionRuleViewModel uiModel);

        bool Edit(CommissionRuleViewModel uiModel);

        bool Delete(int id);

        CommissionRuleViewModel Get(int id);

        IList<CommissionRuleViewModel> GetAll(bool? active);

        IList<CommissionRuleViewModel> GetAllFiltered(
            string filterCommissionBasedOn, string filterCommissionType, string filterActive,
            int currentPage, int pageSize, string sortField,
            string sortDirection, out int totalRecord);
    }
}
