using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Web.Controllers;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class PaymentTypeController : BaseController
    {
        readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [Route("Fill_PaymentType_Combo")]
        public virtual JsonResult Fill_PaymentType_Combo()
        {
            var DataList = _paymentTypeService.GetAll().ToList()
                .Select(x =>
                new SelectListItem
                {
                    Text = x.PaymentTypeName,
                    Value = x.PaymentTypeId.ToString()
                });
            return Json(DataList);
        }
    }
}
