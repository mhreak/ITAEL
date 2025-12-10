using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ApplicantViewModel
    {
        [Display(Name = "شناسه")]
        public int ApplicantId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string LastName { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        public string? FullName { get; set; }

        [Display(Name = "کد ملی")]
        [MaxLength(11, ErrorMessage = "کد ملی باید ۱۰ شماره باشد")]
        public string? NationalCode { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Gender { get; set; }

        [Display(Name = "جنسیت")]
        public string? GenderStr { get; set; }

        [Display(Name = "تلفن ثابت")]
        [StringLength(11, ErrorMessage = "تلفن خانه باید ۱۱ شماره باشد")]
        public string? Phone { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [StringLength(11, ErrorMessage = "تلفن همراه باید ۱۱ شماره باشد")]
        public string Mobile { get; set; }

        [UIHint("DateTime")]
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string? ShamsiBirthDate { get; set; }

        [Display(Name = "شناسه رشته تحصیلی")]
        public int? StudyFieldId { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string StudyFieldName { get; set; }

        [Display(Name = "شناسه استان")]
        public int? ProvinceId { get; set; }

        [Display(Name = "استان")]
        public string? ProvinceName { get; set; }

        [Display(Name = "شناسه شهر")]
        public int? CityId { get; set; }

        [Display(Name = "شهر")]
        public string? CityName { get; set; }

        [StringLength(10)]
        [Display(Name = "کدپستی")]
        public string? PostalCode { get; set; }

        [MaxLength(2000)]
        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "سطح توانایی زبان انگلیسی")]
        public short? EnglishLanguageLevel { get; set; }

        [Display(Name = "سطح زبان انگلیسی")]
        public string? EnglishLanguageLevelStr { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام فایل عکس پرسنلی")]
        public string? PersonalImageFileName { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام فایل روی کارت ملی")]
        public string? NationalCardFrontFileName { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام فایل پشت کارت ملی")]
        public string? NationalCardBackFileName { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام فایل صفحه اول شناسنامه")]
        public string? IdentityCertificateFirstPageFileName { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام فایل صفحه دوم شناسنامه")]
        public string? IdentityCertificateSecondPageFileName { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام فایل اسکن مدرک تحصیلی")]
        public string? EducationalCertificateFileName { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShamsiInsertDate { get; set; } 
    }
}
