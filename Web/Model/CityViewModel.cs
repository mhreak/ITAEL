using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class CityViewModel
    {
        [Display(Name = "شناسه")]
        public int CityId { get; set; }

        [Display(Name = "نام شهرستان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ProvinceId { get; set; }

        [Display(Name = "استان")]
        public string ProvinceName { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }

        [Display(Name = "تعداد داوطلبین")]
        public int ApplicantCount { get; set; }
    }
}
