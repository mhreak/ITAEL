using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
