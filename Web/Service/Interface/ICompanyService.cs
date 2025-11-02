using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface ICompanyService
    {
        int Add(CompanyViewModel uiModel);

        bool Edit(CompanyViewModel uiModel);

        bool Delete(int id);

        CompanyViewModel Get(int id);

        IList<CompanyViewModel> GetAll();

        IList<CompanyViewModel> GetAllFiltered(
            string filterCompanyName,
            string filterInsertDateFrom, string filterInsertDateTo,
            int currentPage, int pageSize, out int totalRecord);

        bool IsDuplicateByCompanyName(int? companyId, string companyName);
    }
}
