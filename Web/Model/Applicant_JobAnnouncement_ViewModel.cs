using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class Applicant_JobAnnouncement_ViewModel
    {
        [Display(Name = "شناسه داوطلب")]
        public int ApplicantId { get; set; }

        [Display(Name = "داوطلب")]
        public string ApplicantFullName { get; set; }

        [Display(Name = "شناسه آگهی")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی")]
        public string JobAnnouncementTitle { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }
    }
}
