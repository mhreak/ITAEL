namespace DbEntities
{
    [Table("SystemSMS")]
    public class SystemSMS
    {
        [Key]
        public int SystemSMSId { get; set; }

        [Required]
        //1 ==> ثبت نام موفق
        public short SMSType { get; set; }

        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }

        [Required]
        public DateTime SendDate { get; set; }
    }
}
