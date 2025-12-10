using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager, Admin")]
    public class OnlineTransactionController(IOnlineTransactionService onlineTransactionService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterApplicantId, string filterExamResourceId,
            string filterDateFrom, string filterDateTo,
            string filterAmountFrom, string filterAmountTo,
            string filterState)
        {
            string sortDirection = "ASC";
            string sortField = "OnlineTransactionId";

            if (request.Sorts != null && request.Sorts.Count > 0)
            {
                sortField = request.Sorts[0].Member;
                sortDirection = request.Sorts[0].SortDirection.ToString();
            }
            int totalRecord = 0;

            var result = new DataSourceResult()
            {
                Data = onlineTransactionService.GetAllFiltered
                (filterApplicantId, filterExamResourceId, filterAmountFrom, filterAmountTo, filterDateFrom, filterDateTo, filterState,
                 currentPage: request.Page, pageSize: request.PageSize, sortField: sortField, sortDirection: sortDirection, totalRecord: out totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [Route("Detail/{id}")]
        public IActionResult Detail(long id)
        {
            var model = onlineTransactionService.Get(id);
            return View(model);
        }
    }
}
