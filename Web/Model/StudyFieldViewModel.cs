using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class StudyFieldViewModel
    {
        [Display(Name = "شناسه")]
        public int StudyFieldId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string StudyFieldName { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }
    }
}
