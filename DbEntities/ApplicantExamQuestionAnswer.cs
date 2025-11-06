namespace DbEntities
{
    [Table("ApplicantExamQuestionAnswer")]
    public class ApplicantExamQuestionAnswer
    {
        [Key]
        [Column(Order = 0)]
        public int ApplicantExamAttemptId { get; set; }

        [ForeignKey("ApplicantExamAttemptId")]
        public ApplicantExamAttempt ApplicantExamAttempt { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ExamQuestionId { get; set; }

        [ForeignKey("ExamQuestionId")]
        public ExamQuestion ExamQuestion { get; set; }

        [MaxLength(4000)]
        public string? AnswerText { get; set; }

        public int? ExamQuestionOptionId { get; set; }

        [ForeignKey("ExamQuestionOptionId")]
        public ExamQuestionOption ExamQuestionOption { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
