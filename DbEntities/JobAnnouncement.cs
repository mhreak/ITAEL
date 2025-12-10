namespace DbEntities
{
    [Table("JobAnnouncement")]
    public class JobAnnouncement
    {
        [Key]
        public int JobAnnouncementId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool? Gender { get; set; }

        public int? Capacity { get; set; }

        [Required]
        public bool ShowCompanyInfo { get; set; }

        [Required]
        public bool HasEmploymentExam { get; set; }

        public DateTime? ExamDate { get; set; }

        //1 ==> حضوری
        //2 ==> غیرحضوری
        public short? JobType { get; set; }

        // 1 ==> تمام وقت
        // 2 ==> پاره وقت
        public short? JobTimeType { get; set; }        

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        public DateTime? JobAnnouncementApplicationDeadlineDateFrom { get; set; }

        public string? ShamsiJobAnnouncementApplicationDeadlineDateFrom { get; set; }

        public DateTime? JobAnnouncementApplicationDeadlineDateTo { get; set; }

        public string? ShamsiJobAnnouncementApplicationDeadlineDateTo { get; set; }

        public decimal? Salary { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public decimal? Price { get; set; }

        public virtual ICollection<InterviewAppointment> InterviewAppointmentList { get; set; } = [];
        public virtual ICollection<JobAnnouncement_Exam> JobAnnouncement_Exam_List { get; set; } = [];
        public virtual ICollection<JobAnnouncement_Skill> JobAnnouncement_Skill_List { get; set; } = [];
        public virtual ICollection<Applicant_JobAnnouncement> Applicant_JobAnnouncement_List { get; set; } = [];
        public virtual ICollection<JobAnnouncement_StudyField> JobAnnouncement_StudyField_List { get; set; } = [];
        public virtual ICollection<JobAnnouncement_ExamResource> JobAnnouncement_ExamResource_List { get; set; } = [];
        public virtual ICollection<JobAnnouncement_JobAnnouncementCategory> JobAnnouncement_JobAnnouncementCategory_List { get; set; } = [];
    }
}
