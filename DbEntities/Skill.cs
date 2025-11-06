namespace DbEntities
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SkillName { get; set; }

        [Required]
        public bool Active { get; set; }

        public virtual ICollection<Skill_ExamResource> Skill_ExamResource_List { get; set; }
        public virtual ICollection<JobAnnouncement_Skill> JobAnnouncement_Skill_List { get; set; }

    }
}
