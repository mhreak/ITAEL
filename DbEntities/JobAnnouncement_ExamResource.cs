using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("JobAnnouncement_ExamResource")]
    public class JobAnnouncement_ExamResource
    {
        [Key]
        [Column(Order = 0)]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public virtual JobAnnouncement JobAnnouncement { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ExamResourceId { get; set; }

        [ForeignKey("ExamResourceId")]
        public virtual ExamResource ExamResource { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
