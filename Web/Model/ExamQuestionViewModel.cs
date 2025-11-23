using DbEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ExamQuestionViewModel
    {
        [Display(Name = "شناسه سوال آزمون")]
        public int ExamQuestionId { get; set; }

        [MaxLength(3000)]
        [Display(Name = "متن")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Text { get; set; }

        [Display(Name = "شناسه آزمون")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ExamId { get; set; }

        [Display(Name = "عنوان آزمون")]
        public string? ExamTitle { get; set; }

        //1 -> چندگزینه ای
        //2 -> تشریحی
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short Type { get; set; }

        [Display(Name = "نوع")]
        public string? TypeStr { get; set; }

        [Display(Name = "ترتیب سوال")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [Range(0, int.MaxValue, ErrorMessage = "عدد باید ۰ یا بزرگتر باشد.")]

        public int QuestionOrder { get; set; }

        public List<ExamQuestionOptionViewModel>? ExamQuestionOptionViewModelList { get; set; }
    }
}
