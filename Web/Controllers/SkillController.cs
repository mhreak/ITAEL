using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Web.Service.Interface;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class SkillController(ISkillService skillService) : BaseController
    {
        [Route("Fill_Skill_Combo")]
        public virtual JsonResult Fill_Skill_Combo(bool? active)
        {
            var DataList = skillService.GetAll(active)
                                       .Select(x =>
                                                   new SelectListItem
                                                   {
                                                       Text = x.SkillName,
                                                       Value = x.SkillId.ToString()
                                                   });
            return Json(DataList);
        }
    }
}
