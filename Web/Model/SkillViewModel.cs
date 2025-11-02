using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class SkillViewModel
    {
        [Display(Name = "شناسه")]
        public int SkillId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string SkillName { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }
    }
}
