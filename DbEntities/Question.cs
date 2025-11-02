using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3000)]
        public string Text { get; set; }

        //1 -> چندگزینه ای
        //2 -> تشریحی
        [Required]
        [StringLength(50)]
        public short Type { get; set; }

        [Required]
        public string Options { get; set; } // JSON or comma-separated for choices

        [Required]
        public string CorrectAnswer { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
    }
}
