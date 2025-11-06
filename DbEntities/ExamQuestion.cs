namespace DbEntities
{
    [Table("ExamQuestion")]
    public class ExamQuestion
    {
        [Key]
        public int ExamQuestionId { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Text { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        //1 -> چندگزینه ای
        //2 -> تشریحی
        [Required]
        public short Type { get; set; }

        [Required]
        public int QuestionOrder { get; set; }

        public virtual ICollection<ExamQuestionOption> ExamQuestionOptionList { get; set; } = [];

        public virtual ICollection<ApplicantExamQuestionAnswer> ApplicantExamQuestionAnswerList { get; set; } = [];
    }
}
