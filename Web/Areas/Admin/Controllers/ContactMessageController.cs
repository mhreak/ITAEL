using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Controllers;
using Web.Service;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ContactMessageController(IContactMessageService contactMessageService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = contactMessageService.GetAll();
            var result = new DataSourceResult()
                         {
                             Data = data,
                             Total = data.Count // Total number of records
            };

            return Json(result);
        }

        [Route("Delete")]
        public bool Delete(int id)
        {
            return contactMessageService.Delete(id);
        }
    }
}
