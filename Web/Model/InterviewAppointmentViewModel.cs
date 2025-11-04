using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class InterviewAppointmentViewModel
    {
        [Display(Name = "شناسه قرار مصاحبه")]
        public int InterviewAppointmentId { get; set; }

        [Display(Name = "شناسه آگهی شغلی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی شغلی")]
        public string? JobAnnouncementTitle { get; set; }

        [Display(Name = "شناسه داوطلب")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ApplicantId { get; set; }

        [Display(Name = "نام داوطلب")]
        public string? ApplicantFullName { get; set; }

        //1 -> درحال بررسی
        //2 -> تأیید شده 
        //3 -> لغو شده
        //4 -> تکمیل شده
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short Status { get; set; }

        [Display(Name = "وضعیت")]
        public string? StatusStr { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShamsiInsertDate { get; set; }
    }
}
