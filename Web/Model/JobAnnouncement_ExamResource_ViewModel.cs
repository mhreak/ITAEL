using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class JobAnnouncement_ExamResource_ViewModel
    {
        [Display(Name = "شناسه آگهی")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی")]
        public string? JobAnnouncementTitle { get; set; }

        [Display(Name = "شناسه منبع آزمون")]
        public int ExamResourceId { get; set; }

        [Display(Name = "نام منبع")]
        public string? ResourceName { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShmasiInsertDate { get; set; }
    }
}
