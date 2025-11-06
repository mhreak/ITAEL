namespace DbEntities
{
    [Table("ExamQuestionOption")]
    public class ExamQuestionOption
    {
        [Key]
        public int ExamQuestionOptionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public int ExamQuestionId { get; set; }

        [ForeignKey("ExamQuestionId")]
        public ExamQuestion ExamQuestion { get; set; }

        [Required]
        public short Order { get; set; }

        [Required]
        public bool IsCorrectAnswer { get; set; }

        public virtual ICollection<ApplicantExamQuestionAnswer> ApplicantExamQuestionAnswerList { get; set; } = [];
    }
}
