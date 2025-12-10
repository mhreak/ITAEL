using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IExamResourceOrderItemService
    {
        int Add(ExamResourceOrderItemViewModel uiModel);

        bool Edit(ExamResourceOrderItemViewModel uiModel);

        bool Delete(int examResourceOrderItemId);

        ExamResourceOrderItemViewModel Get(int examResourceOrderItemId);

        ExamResourceOrderItemViewModel Get(int examResourceOrderId, int examResourceOrderItemId);

        IList<ExamResourceOrderItemViewModel> GetAllByExamResourceId(int examResourceId);

        IList<ExamResourceOrderItemViewModel> GetAllByExamResourceOrderId(int examResourceOrderId);

        IList<ExamResourceOrderItemViewModel> GetAllFiltered(string filterExamResourceId, string filterExamResourceOrderId,
                                                             string filterInsertDateFrom, string filterInsertDateTo,
                                                             int currentPage, int pageSize, out int totalRecord);
    }
}
