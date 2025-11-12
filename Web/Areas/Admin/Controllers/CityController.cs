using System.Linq;

using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    public class CityController(ICityService cityService) : BaseController
    {
        [Route("Fill_City_Combo")]
        public IActionResult Fill_City_Combo(int provinceId)
        {
            var DataList = cityService.GetAllByProvinceId(provinceId)
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
