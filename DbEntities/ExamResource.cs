namespace DbEntities
{
    [Table("ExamResource")]
    public class ExamResource
    {
        [Key]
        public int ExamResourceId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ResourceName { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        //1 -> فیزیکی
        //2 -> مجازی
        [Required]
        public short Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        //فقط برای منابع مجازی
        [MaxLength(100)]
        public string? DownloadLink { get; set; }

        [MaxLength(50)]
        public string? ImageFileName { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        public virtual ICollection<ExamResourceOrder> ExamResourceOrderList { get; set; } = [];
        public virtual ICollection<Skill_ExamResource> Skill_ExamResource_List { get; set; } = [];
        public virtual ICollection<StudyField_ExamResource> StudyField_ExamResource_List { get; set; } = [];
        public virtual ICollection<JobAnnouncement_ExamResource> JobAnnouncement_ExamResource_List { get; set; } = [];
    }
}
