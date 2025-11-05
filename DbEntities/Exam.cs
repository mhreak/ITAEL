using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public virtual ICollection<ExamQuestion> ExamQuestionList { get; set; } = [];
        public virtual ICollection<ApplicantExamAttempt> ApplicantAttemptList { get; set; } = [];
        public virtual ICollection<JobAnnouncement_Exam> JobAnnouncement_Exam_List { get; set; } = [];
    }
}
