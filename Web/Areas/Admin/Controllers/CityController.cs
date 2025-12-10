using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class CityController(ICityService cityService) : BaseController
    {
        [HttpGet]
        [Route("Fill_City_Combo")]
        public IActionResult Fill_City_Combo(int provinceId)
        {
            var DataList = cityService.GetAllByProvinceId(provinceId)
                                      .Select(x =>
                                                  new SelectListItem
                                                  {
                                                      Text = x.CityName,
                                                      Value = x.CityId.ToString()
                                                  });
            return Json(DataList);
        }
    }
}
