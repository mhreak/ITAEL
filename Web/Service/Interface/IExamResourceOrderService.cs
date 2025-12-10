using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IExamResourceOrderService
    {
        int Add(ExamResourceOrderViewModel uiModel);

        bool Edit(ExamResourceOrderViewModel uiModel);

        bool Delete(int id);

        bool SetStatus(int examResourceOrderId, short status);

        ExamResourceOrderViewModel Get(int id);

        IList<ExamResourceOrderViewModel> GetAll();

        IList<ExamResourceOrderViewModel> GetAllByApplicantId(int applicantId);

        IList<ExamResourceOrderViewModel> GetAllFiltered(string filterApplicantId,
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
