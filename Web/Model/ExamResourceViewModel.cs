using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Model
{
    public class ExamResourceViewModel
    {
        [Display(Name = "شناسه منبع آزمون")]
        public int ExamResourceId { get; set; }

        [MaxLength(200)]
        [Display(Name = "نام منبع")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string ResourceName { get; set; }

        [MaxLength(2000)]
        [Display(Name = "جزئیات")]
        public string? Description { get; set; }

        //1 -> فیزیکی
        //2 -> مجازی
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short Type { get; set; }

        [Display(Name = "نوع")]
        public string? TypeStr { get; set; }

        [Display(Name = "قیمت")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        //فقط برای منابع مجازی
        [MaxLength(100)]
        [Display(Name = "لینک دانلود")]
        public string? DownloadLink { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام فایل عکس")]
        public string? ImageFileName { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShamsiInsertDate { get; set; }
    }
}
