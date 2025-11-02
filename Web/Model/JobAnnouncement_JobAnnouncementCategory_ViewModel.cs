using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class JobAnnouncement_JobAnnouncementCategory_ViewModel
    {
        [Display(Name = "شناسه آگهی")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی")]
        public string JobAnnouncementTitle { get; set; }

        [Display(Name = "شناسه دسته بندی")]
        public int JobAnnouncementCategoryId { get; set; }

        [Display(Name = "نام دسته بندی")]
        public string JobAnnouncementCategoryName { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }
    }
}
