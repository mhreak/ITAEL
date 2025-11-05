using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ExamViewModel
    {
        [Display(Name = "شناسه آزمون")]
        public int ExamId { get; set; }

        [MaxLength(200)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Title { get; set; }

        [MaxLength(2000)]
        [Display(Name = "جزئیات")]
        public string? Description { get; set; }
    }
}
