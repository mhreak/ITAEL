using System.ComponentModel.DataAnnotations;

namespace Web.Model.Identity
{
    public class ApplicationUserViewModel
    {
        [Display(Name = "شناسه کاربر")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "شناسه مسافر")]
        public int? PassengerId { get; set; }

        [MaxLength(70)]
        [Display(Name = "شرکت/ سازمان")]
        public string CompanyName { get; set; }

        [Required]
        //-1 ==> rejected
        //0 ==> 
        //1 ==> accepted
        public short AccountState { get; set; }

        [Display(Name = "نقش")]
        public string RoleName { get; set; }

        [Display(Name = "تلفن تماس")]
        public string PhoneNumber { get; set; }
    }
}
