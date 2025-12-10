using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DbEntities
{
    [Table("SystemSMS")]
    public class SystemSMS
    {
        [Key]
        public int SystemSMSId { get; set; }

        [Required]
        //1 ==> لیست قیمت هوشمند
        //2 ==> تبریک تولد
        //3 ==> ثبت درخواست فعالسازی گارانتی
        //4 ==> رد شدن درخواست فعالسازی گارانتی
        //5 ==> فعال شدن گارانتی
        //6 ==> پایان دوره گارانتی
        //7 ==> ارسال زیلینک
        //8 ==> ارسال لینک لیست قیمت
        //9 ==> پیامک های رهگیری سفارش
        public short SMSType { get; set; }

        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }

        [Required]
        public DateTime SendDate { get; set; }
    }
}
