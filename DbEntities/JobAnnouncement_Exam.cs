namespace DbEntities
{
    [Table("JobAnnouncement_Exam")]
    public class JobAnnouncement_Exam
    {
        [Key]
        [Column(Order = 0)]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public virtual JobAnnouncement JobAnnouncement { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ExamId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        //سوالات رندوم
        [Required]
        public bool RandomizeQuestions { get; set; }

        //جواب های رندوم
        [Required]
        public bool RandomizeOptions { get; set; }

        //برگشتن به سؤال قبل
        [Required]
        public bool AllowNavigateToPreviousQuestion { get; set; }
    }
}
