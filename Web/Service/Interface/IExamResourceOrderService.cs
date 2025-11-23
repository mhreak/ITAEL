using Web.Model;
using System.Collections.Generic;

namespace Web.Service.Interface
{
    public interface IExamResourceOrderService
    {
        int Add(ExamResourceOrderViewModel uiModel);

        bool Edit(ExamResourceOrderViewModel uiModel);

        bool Delete(int id);

        ExamResourceOrderViewModel Get(int id);

        IList<ExamResourceOrderViewModel> GetAll();

        IList<ExamResourceOrderViewModel> GetAllFiltered(string filterApplicantId,
                                                         string filterExamResourceId,
                                                         string filterStatus,
                                                         string filterTotalPriceFrom,
                                                         string filterTotalPriceTo,
                                                         string filterOrderDateFrom,
                                                         string filterOrderDateTo,
                                                         string filterDeliveryDateFrom,
                                                         string filterDeliveryDateTo,
                                                         int currentPage,
                                                         int pageSize,
                                                         out int totalRecord);
    }
}
