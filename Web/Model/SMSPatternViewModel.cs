using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class SMSPatternViewModel
    {
        [Key]
        public int SMSPatternId { get; set; }
        
        [MaxLength(50)]
        public string? SMSPatternName { get; set; }

        [MaxLength(50)]
        public string? PatternCode { get; set; }

        //1 ==> لیست قیمت هوشمند
        //2 ==> تبریک تولد
        //3 ==> ثبت درخواست فعالسازی گارانتی
        //4 ==> رد شدن درخواست فعالسازی گارانتی
        //5 ==> فعال شدن گارانتی
        //6 ==> پایان دوره گارانتی
        //7 ==> ارسال زیلینک
        //8 ==> ارسال لینک لیست قیمت
        public short? TemplateType { get; set; }

        public string? TemplateText { get; set; }
    }
}
