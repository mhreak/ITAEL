using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("ExamResource")]
    public class ExamResource
    {
        [Key]
        public int ExamResourceId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        //1 -> فیزیکی
        //2 -> مجازی
        [Required]
        [StringLength(50)]
        public short Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //فقط برای منابع مجازی
        [StringLength(500)]
        public string? DownloadLink { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;

        public virtual ICollection<ExamResourceOrder> ExamResourceOrderList { get; set; } = [];
        public virtual ICollection<Skill_ExamResource> Skill_ExamResource_List { get; set; } = [];
        public virtual ICollection<StudyField_ExamResource> StudyField_ExamResource_List { get; set; } = [];
        public virtual ICollection<JobAnnouncement_ExamResource> JobAnnouncement_ExamResource_List { get; set; } = [];
    }
}
