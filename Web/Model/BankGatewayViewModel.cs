using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class BankGatewayViewModel
    {
        [Display(Name = "شناسه")]
        public int BankGatewayId { get; set; }

        [Display(Name = "نام درگاه پرداخت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۵۰ کاراکتر است")]
        public string GatewayName { get; set; }

        [Display(Name = "نوع درگاه پرداخت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        //1 ==> zarinpal
        public short GatewayType { get; set; }

        [Display(Name = "نوع درگاه پرداخت")]
        public string GatewayTypeStr { get; set; }

        [Display(Name = "پارامتر اول")]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۱۰۰ کاراکتر است")]
        public string Parameter1 { get; set; }

        [Display(Name = "پارامتر دوم")]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۱۰۰ کاراکتر است")]
        public string Parameter2 { get; set; }

        [Display(Name = "پارامتر سوم")]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۱۰۰ کاراکتر است")]
        public string Parameter3 { get; set; }

        [Display(Name = "پارامتر چهارم")]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۱۰۰ کاراکتر است")]
        public string Parameter4 { get; set; }

        [Display(Name = "پارامتر پنجم")]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۱۰۰ کاراکتر است")]
        public string Parameter5 { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }
    }
}
