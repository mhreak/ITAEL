namespace DbEntities
{
    public class JobAnnouncement_StudyField
    {
        [Key]
        [Column(Order = 0)]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public virtual JobAnnouncement JobAnnouncement { get; set; }

        [Key]
        [Column(Order = 1)]
        public int StudyFieldId { get; set; }

        [ForeignKey("StudyFieldId")]
        public virtual StudyField StudyField { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
