using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table(name: "Applicant_JobAnnouncement")]
    public class Applicant_JobAnnouncement
    {
        [Key]
        [Column(Order = 0)]
        public int ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }

        [Key]
        [Column(Order = 1)]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public virtual JobAnnouncement JobAnnouncement { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }



        public virtual ICollection<Applicant_JobAnnouncement> Applicant_JobAnnouncement_List { get; set; }
    }
}
