using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Web.Controllers;
using Web.Service.Interface;

namespace Web.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    [Route("Applicant/[controller]")]
    [Authorize(Roles = "Applicant")]
    public class CityController(ICityService cityService) : BaseController
    {
        // Controller
        [Route("Fill_City_Combo")]
        public IActionResult Fill_City_Combo(int? ProvinceId) // nullable برای جلوگیری از خطا
        {
            if (!ProvinceId.HasValue || ProvinceId.Value == 0)
                return Json(new List<SelectListItem>());

            var DataList = cityService.GetAllByProvinceId(ProvinceId.Value)
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
