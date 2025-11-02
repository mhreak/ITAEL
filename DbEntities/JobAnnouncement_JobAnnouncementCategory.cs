using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbEntities
{
    [Table("JobAnnouncement_JobAnnouncementCategory")]
    public class JobAnnouncement_JobAnnouncementCategory
    {
        [Key]
        [Column(Order = 0)]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public virtual JobAnnouncement JobAnnouncement { get; set; }

        [Key]
        [Column(Order = 1)]
        public int JobAnnouncementCategoryId { get; set; }

        [ForeignKey("JobAnnouncementCategoryId")]
        public virtual JobAnnouncementCategory JobAnnouncementCategory { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
