using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("JobAnnouncementCategory")]
    public class JobAnnouncementCategory
    {
        [Key]
        public int JobAnnouncementCategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public bool Active { get; set; }



        public virtual ICollection<JobAnnouncement_JobAnnouncementCategory> JobAnnouncement_JobAnnouncementCategory_List { get; set; }
    }
}
