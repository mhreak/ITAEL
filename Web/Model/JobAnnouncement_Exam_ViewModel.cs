using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class JobAnnouncement_Exam_ViewModel
    {
        [Display(Name = "شناسه آگهی")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی")]
        public string? JobAnnouncementTitle { get; set; }

        [Display(Name = "شناسه آزمون")]
        public int ExamId { get; set; }

        [Display(Name = "عنوان آزمون")]
        public string? ExamTitle { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShamsiInsertDate { get; set; }
    }
}
