using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class AboutUsSettingViewModel
    {
        [MaxLength(5000)]
        [Display(Name = "جزئیات")]
        public string Description { get; set; }
    }
}
