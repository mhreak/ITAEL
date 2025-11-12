using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Controllers;

namespace Web.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    [Route("Applicant/[controller]")]
    [Authorize(Roles = "Applicant,Admin")]
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
