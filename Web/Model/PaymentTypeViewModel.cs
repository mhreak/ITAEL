using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class PaymentTypeViewModel
    {
        [Display(Name = "شناسه")]
        public int PaymentTypeId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string PaymentTypeName { get; set; }
    }
}
