using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Web.Service.Interface;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class JobAnnouncementCategoryController(IJobAnnouncementCategoryService jobAnnouncementCategoryService) : BaseController
    {
        [Route("Fill_JobAnnouncementCategory_Combo")]
        public virtual JsonResult Fill_JobAnnouncementCategory_Combo(bool? active)
        {
            var DataList = jobAnnouncementCategoryService.GetAll(active)
                                                         .Select(x =>
                                                                     new SelectListItem
                                                                     {
                                                                         Text = x.CategoryName,
                                                                         Value = x.JobAnnouncementCategoryId.ToString()
                                                                     });
            return Json(DataList);
        }
    }
}
