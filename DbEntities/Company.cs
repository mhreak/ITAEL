using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("Company")]
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


        public virtual ICollection<Collaborator> CollaboratorList { get; set; }
        public virtual ICollection<JobAnnouncement> JobAnnouncementList { get; set; }
    }
}
