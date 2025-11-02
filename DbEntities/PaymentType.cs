using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table(name: "PaymentType")]
    public class PaymentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentTypeName { get; set; }
    }
}
