using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class JobAnnouncement_Skill_ViewModel
    {
        [Display(Name = "شناسه آگهی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی")]
        public string JobAnnouncementTitle { get; set; }

        [Display(Name = "شناسه مهارت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int SkillId { get; set; }

        [Display(Name = "نام مهارت")]
        public string SkillName { get; set; }

        [Display(Name = "هزینه ثبت نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int RegistrationAmount { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }
    }
}
