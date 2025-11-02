using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class SystemSMSController(ISystemSMSService systemSMSService) : BaseController
    {
        readonly ISystemSMSService _systemSMSService = systemSMSService;

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
