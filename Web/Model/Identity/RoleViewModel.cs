using System.ComponentModel.DataAnnotations;

namespace Web.Model.Identity
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نقش کاربری")]
        [MaxLength(256)]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Name { get; set; }

        [MaxLength(256)]
        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
