using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class SettingViewModel
    {
        [Display(Name = "شناسه")]
        public int SettingId { get; set; }

        [MaxLength(100)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} باید حتما وارد شود")]
        public string SettingName { get; set; }

        [Display(Name = "کلید")]
        [Required(ErrorMessage = "{0} باید حتما وارد شود")]
        public string SettingKey { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "{0} باید حتما وارد شود")]
        public string SettingValue { get; set; }
    }
}
