using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ProvinceViewModel
    {
        [Display(Name = "شناسه استان")]
        public int ProvinceId { get; set; }

        [Display(Name = "نام استان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز 2 کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز 50 کاراکتر است")]
        public string ProvinceName { get; set; }

        [Display(Name = "تعداد شهرستان ها")]
        public int CityCount { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }
    }
}
