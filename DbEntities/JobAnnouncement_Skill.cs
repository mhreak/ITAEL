using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("JobAnnouncement_Skill")]
    public class JobAnnouncement_Skill
    {
        [Key]
        [Column(Order = 0)]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public virtual JobAnnouncement JobAnnouncement { get; set; }

        [Key]
        [Column(Order = 1)]
        public int SkillId { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }

        [Required]
        public int RegistrationAmount { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
