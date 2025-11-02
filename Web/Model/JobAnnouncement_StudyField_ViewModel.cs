using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class JobAnnouncement_StudyField_ViewModel
    {
        [Display(Name = "شناسه")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی")]
        public string JobAnnouncementTitle { get; set; }

        [Display(Name = "شناسه رشته تحصیلی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int StudyFieldId { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string StudyFieldName { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }
    }
}
