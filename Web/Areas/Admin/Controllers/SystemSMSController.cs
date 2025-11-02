using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class SystemSMSController : BaseController
    {
        readonly ISystemSMSService _systemSMSService;

        public SystemSMSController(ISystemSMSService systemSMSService)
        {
            _systemSMSService = systemSMSService;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
