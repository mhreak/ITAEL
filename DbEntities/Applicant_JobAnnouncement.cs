namespace DbEntities
{
    [Table(name: "Applicant_JobAnnouncement")]
    public class Applicant_JobAnnouncement
    {
        [Key]
        [Column(Order = 0)]
        public int ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }

        [Key]
        [Column(Order = 1)]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public virtual JobAnnouncement JobAnnouncement { get; set; }

        //-1 -> رد شده
        //0 -> نامشخص
        //1 -> قبول شده
        [Required]
        public short Status { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
