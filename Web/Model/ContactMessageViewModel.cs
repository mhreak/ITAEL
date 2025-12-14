using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ContactMessageViewModel
    {
        [Display(Name = "شناسه پیام فرم تماس")]
        public int ContactMessageId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]

        public string Name { get; set; }

        [Display(Name = "موبایل")]
        [StringLength(11)]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Mobile { get; set; }

        [MaxLength(4000)]
        [Display(Name = "متن پیام فرم تماس")]
        [Required(ErrorMessage = "این فیلد الزامی است")]

        public string Text { get; set; }
    }
}
