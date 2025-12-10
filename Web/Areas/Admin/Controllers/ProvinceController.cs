using System.Linq;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ProvinceController(IProvinceService provinceService) : BaseController
    {
        [Route("Fill_Province_Combo")]
        public IActionResult Fill_Province_Combo()
        {
            var DataList = provinceService.GetAll()
                                                       .Select(x =>
                                                                   new SelectListItem
                                                                   {
                                                                       Text = x.ProvinceName,
                                                                       Value = x.ProvinceId.ToString()
                                                                   });
            return Json(DataList);
        }
    }
}
