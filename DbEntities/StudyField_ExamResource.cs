using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("StudyField_ExamResource")]
    public class StudyField_ExamResource
    {
        [Key]
        [Column(Order = 0)]
        public int StudyFieldId { get; set; }

        [ForeignKey("StudyFieldId")]
        public virtual StudyField StudyField { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ExamResourceId { get; set; }

        [ForeignKey("ExamResourceId")]
        public virtual ExamResource ExamResource { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
