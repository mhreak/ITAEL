using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbEntities
{
    public class ExamResourceOrderItem
    {
        [Key]
        public int ExamResourceOrderItemId { get; set; }

        [Required]
        public int ExamResourceId { get; set; }

        [ForeignKey("ExamResourceId")]
        public ExamResource ExamResource { get; set; }

        [Required]
        public int ExamResourceOrderId { get; set; }

        [ForeignKey("ExamResourceOrderId")]
        public ExamResourceOrder ExamResourceOrder { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
