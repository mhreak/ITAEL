using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class JobAnnouncementViewModel
    {
        [Display(Name = "شناسه")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "شناسه شرکت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int CompanyId { get; set; }

        [Display(Name = "شرکت")]
        public string CompanyName { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز ۱۰۰ کاراکتر است")]
        public string Title { get; set; }

        [Display(Name = "متن آگهی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(3000, ErrorMessage = "حداکثر طول مجاز ۳۰۰۰ کاراکتر است")]
        public string Description { get; set; }

        [Display(Name = "تاریخ انتشار")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "تاریخ انتشار")]
        public string ShamsiPublishDate { get; set; }

        [Display(Name = "تاریخ انقضا")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "تاریخ انقضا")]
        public string ShamsiExpirationDate { get; set; }

        [Display(Name = "جنسیت")]
        public bool? Gender { get; set; }

        [Display(Name = "جنسیت")]
        public string GenderStr { get; set; }

        [UIHint("Number")]
        [Display(Name = "ظرفیت")]
        public int? Capacity { get; set; }

        [Display(Name = "نمایش اطلاعات شرکت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool ShowCompanyInfo { get; set; }

        [Display(Name = "آزمون دارد")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool HasEmploymentExam { get; set; }

        [Display(Name = "تاریخ آزمون")]
        public DateTime? ExamDate { get; set; }

        [Display(Name = "تاریخ آزمون")]
        public string ShamsiExamDate { get; set; }

        //1 ==> حضوری
        //2 ==> غیرحضوری
        [Display(Name = "نوع کار")]
        public short? JobType { get; set; }

        [Display(Name = "نوع کار")]
        public string JobTypeStr { get; set; }

        // 1 ==> تمام وقت
        // 2 ==> پاره وقت
        [Display(Name = "زمان کار")]
        public short? JobTimeType { get; set; }

        [Display(Name = "زمان کار")]
        public string JobTimeTypeStr { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
