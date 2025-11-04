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

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        //سوالات رندوم
        [Required]
        public bool RandomizeQuestions { get; set; }

        //جواب های رندوم
        [Required]
        public bool RandomizeOptions { get; set; }

        //برگشتن به سؤال قبل
        [Required]
        public bool AllowNavigateToPreviousQuestion { get; set; }

        public virtual ICollection<ExamQuestion> ExamQuestionList { get; set; } = [];
        public virtual ICollection<ApplicantExamAttempt> ApplicantAttemptList { get; set; } = [];
        public virtual ICollection<JobAnnouncement_Exam> JobAnnouncement_Exam_List { get; set; } = [];
    }
}
