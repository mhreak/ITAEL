using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Model.Identity
{
    public class SignupViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۵۰ کاراکتر است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۵۰ کاراکتر است")]
        public string LastName { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [StringLength(10, ErrorMessage = "کد ملی باید ۱۰ رقم باشد")]
        [Remote("IsUserNameInUse", "Account", HttpMethod = "POST", AdditionalFields = "__RequestVerificationToken")]
        public string NationalCode { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Gender { get; set; }

        [Phone]
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [StringLength(11, ErrorMessage = "تلفن همراه باید ۱۱ شماره باشد")]
        [RegularExpression("^([0-9]{11})$", ErrorMessage = "مقدار واردشده برای تلفن همراه نامعتبر است")]
        public string Mobile { get; set; }

        [Display(Name = "شناسه سازمان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int OrganizationId { get; set; }

        [Display(Name = "سازمان")]
        public string OrganizationName { get; set; }

        [Display(Name = "بخش")]
        [MaxLength(70, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۷۰ کاراکتر است")]
        public string DepartmentName { get; set; }

        [Display(Name = "ناحیه")]
        [MaxLength(70, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۷۰ کاراکتر است")]
        public string SubDepartmentName { get; set; }

        [Display(Name = "پرسنل سازمان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool IsPersonnel { get; set; }

        [Display(Name = "عنوان شغلی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short JobTitle { get; set; }

        [Display(Name = "کد پرسنلی")]
        [MaxLength(30, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۳۰ کاراکتر است")]
        public string PersonnelCode { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MaxLength(30, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۳۰ کاراکتر است")]
        public string Address_City { get; set; }

        [Display(Name = "خیابان اصلی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۵۰ کاراکتر است")]
        public string Address_Avenue { get; set; }

        [Display(Name = "خیابان فرعی")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۵۰ کاراکتر است")]
        public string Adderss_Street { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MaxLength(200, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۲۰۰ کاراکتر است")]
        public string Address { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public Decimal Address_Latitude { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public Decimal Address_Longitude { get; set; }

        [MaxLength(10)]
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string PostalCode { get; set; }

        [Required]
        // 1 ==> ندارم
        // 2 ==> حرکت با ویلچر
        // 3 ==> مشکل شنوایی
        // 4 ==> سایر
        [Display(Name = "مشکلات جسمانی")]
        public short DisablityStatus { get; set; }

        [Display(Name = "شیفت کاری")]
        public int? WorkShiftId { get; set; }

        [Display(Name = "زمان آغاز کار روزانه")]
        public TimeSpan? StartWorkTime { get; set; }

        [Display(Name = "زمان پایان کار روزانه")]
        public TimeSpan? EndWorkTime { get; set; }

        public bool? WorkDay_Saturday { get; set; }

        public bool? WorkDay_Sunday { get; set; }

        public bool? WorkDay_Monday { get; set; }

        public bool? WorkDay_Tuesday { get; set; }

        public bool? WorkDay_Wednesday { get; set; }

        public bool? WorkDay_Thursday { get; set; }

        public bool? WorkDay_Friday { get; set; }

        public bool? WorkDay_Holiday { get; set; }

        //public string IsShiftWorking { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork1Start { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork1Finish { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork2Start { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork2Finish { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork3Start { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork3Finish { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork4Start { get; set; }

        //[MaxLength(5)]
        //public string ShiftWork4Finish { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [Remote(action: "ValidatePassword", controller: "Signup", areaName: "")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار رمز عبور یکسان نیستند")]
        public string ConfirmPassword { get; set; }
    }
}
