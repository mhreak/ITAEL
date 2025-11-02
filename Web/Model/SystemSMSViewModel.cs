using System.ComponentModel.DataAnnotations;
using System;

namespace Web.Model
{
    public class SystemSMSViewModel
    {
        [Display(Name = "شناسه")]
        public int SystemSMSId { get; set; }

        //1 ==> لیست قیمت هوشمند
        //2 ==> تبریک تولد
        //3 ==> ثبت درخواست فعالسازی گارانتی
        //4 ==> رد شدن درخواست فعالسازی گارانتی
        //5 ==> فعال شدن گارانتی
        //6 ==> پایان دوره گارانتی
        //7 ==> ارسال زیلینک
        //8 ==> ارسال لینک لیست قیمت
        [Display(Name = "نوع پیامک")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short SMSType { get; set; }

        [Display(Name = "نوع پیامک")]
        public string SMSTypeStr { get; set; }

        [Display(Name = "تلفن همراه")]
        [MaxLength(11, ErrorMessage = "تلفن همراه باید ۱۱ شماره باشد")]
        public string Mobile { get; set; }

        [Display(Name = "تاریخ و زمان ارسال")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime SendDate { get; set; }

        [Display(Name = "تاریخ و زمان ارسال")]
        public string ShamsiSendDate { get; set; }
    }
}
