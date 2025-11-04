using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class StudyField_ExamResource_ViewModel
    {
        [Display(Name = "شناسه رشته تحصیلی")]
        public int StudyFieldId { get; set; }

        [Display(Name = "نام رشته تحصیلی")]
        public string? StudyFieldName { get; set; }

        [Display(Name = "شناسه منبع آزمون")]
        public int ExamResourceId { get; set; }

        [Display(Name = "نام منبع")]
        public string? ResourceName { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShmasiInsertDate { get; set; }
    }
}
