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
        [StringLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        //سوالات رندوم
        [Required]
        public bool RandomizeQuestions { get; set; }

        //جواب های رندوم
        [Required]
        public bool RandomizeOptions { get; set; }

        public virtual ICollection<Question> QuestionList { get; set; } = [];
        public virtual ICollection<ApplicantExamAttempt> ApplicantAttempts { get; set; } = [];
        public virtual ICollection<JobAnnouncement_Exam> JobAnnouncement_Exam_List { get; set; } = [];
    }
}
