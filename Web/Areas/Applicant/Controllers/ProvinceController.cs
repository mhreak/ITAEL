using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Controllers;
using Web.Service.Interface;

namespace Web.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    [Route("Applicant/[controller]")]
    [Authorize(Roles = "Applicant")]
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
