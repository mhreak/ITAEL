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
    public class StudyFieldController(IStudyFieldService studyFieldService) : BaseController
    {
        [Route("Fill_StudyField_Combo")]
        public virtual JsonResult Fill_StudyField_Combo(bool? active)
        {
            var DataList = studyFieldService.GetAll(active)
                                            .Select(x =>
                                                        new SelectListItem
                                                        {
                                                            Text = x.StudyFieldName,
                                                            Value = x.StudyFieldId.ToString()
                                                        });
            return Json(DataList);
        }
    }
}
