namespace DbEntities
{
    [Table("ApplicantExamAttempt")]
    public class ApplicantExamAttempt
    {
        [Key]
        public int ApplicantExamAttemptId { get; set; }

        [Required]
        public int ApplicantId { get; set; }

        [Required]
        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        public decimal FinalScore { get; set; }

        //فقط برای زمانی که سوالات رندوم باشد.
        public string? QuestionsOrder { get; set; }

        //1 -> قبول شده
        //2 -> رد شده
        //3 -> در حال بررسی
        //4 -> آزمون تمام شده
        //5 -> آزمون تمام نشده
        [Required]
        public short Status { get; set; }

        public virtual ICollection<ApplicantExamQuestionAnswer> ApplicantExamQuestionAnswerList { get; set; } = [];
    }
}
