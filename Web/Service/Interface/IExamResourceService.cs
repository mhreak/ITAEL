using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IExamResourceService
    {
        int Add(ExamResourceViewModel uiModel);

        bool Edit(ExamResourceViewModel uiModel);

        bool Delete(int id);

        ExamResourceViewModel Get(int id);

        IList<ExamResourceViewModel> GetAll();

        public IList<ExamResourceViewModel> GetAllFiltered(string filterResourceName, string filterDescription,
                                                           string filterType, string filterPriceFrom, string filterPriceTo,
                                                           string filterInsertDateFrom, string filterInsertDateTo,
                                                           int currentPage, int pageSize, out int totalRecord);

        bool SetImageFileName(int examResourceId, string fileName);
    }
}
