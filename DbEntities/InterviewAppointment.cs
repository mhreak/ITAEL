namespace DbEntities
{
    [Table("InterviewAppointment")]
    public class InterviewAppointment
    {
        [Key]
        public int InterviewAppointmentId { get; set; }

        [Required]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public JobAnnouncement JobAnnouncement { get; set; }

        [Required]
        public int ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }

        //1 -> درحال بررسی
        //2 -> تأیید شده 
        //3 -> لغو شده
        //4 -> تکمیل شده
        [Required]
        public short Status { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
