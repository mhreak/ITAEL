using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("StudyField")]
    public class StudyField
    {
        [Key]
        public int StudyFieldId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StudyFieldName { get; set; }

        [Required]
        public bool Active { get; set; }



        public virtual ICollection<Applicant> ApplicantList { get; set; }
        public virtual ICollection<JobAnnouncement_StudyField> JobAnnouncement_StudyField_List { get; set; }
    }
}
