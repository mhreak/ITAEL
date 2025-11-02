using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("Skill_ExamResource")]
    public class Skill_ExamResource
    {
        [Key]
        [Column(Order = 0)]
        public int SkillId { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ExamResourceId { get; set; }

        [ForeignKey("ExamResourceId")]
        public virtual ExamResource ExamResource { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
