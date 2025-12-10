using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table(name: "BankGateway")]
    public class BankGateway
    {
        [Key]
        public int BankGatewayId { get; set; }

        [Required]
        [MaxLength(50)]
        public string GatewayName { get; set; }

        [Required]
        //1 ==> zarinpal
        public short GatewayType { get; set; }

        [MaxLength(100)]
        public string Parameter1 { get; set; }

        [MaxLength(100)]
        public string Parameter2 { get; set; }

        [MaxLength(100)]
        public string Parameter3 { get; set; }

        [MaxLength(100)]
        public string Parameter4 { get; set; }

        [MaxLength(100)]
        public string Parameter5 { get; set; }

        [Required]
        public bool Active { get; set; }

        public virtual ICollection<OnlineTransaction> OnlineTransactionList { get; set; }
        public virtual ICollection<ExamResourceOrder> ExamResourceOrderList { get; set; }
    }
}
