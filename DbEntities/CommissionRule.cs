using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("CommissionRule")]
    public class CommissionRule
    {
        [Key]
        public int CommissionRuleId { get; set; }

        [Required]
        // 1 ==> تعداد ثبت نام
        // 2 ==> مبلغ ثبت نام
        public short CommissionBasedOn { get; set; }

        public long? MinimumAmount { get; set; }

        public long? MaximumAmount { get; set; }

        public int? MinimumNumber { get; set; }

        public int? MaximumNumber { get; set; }

        [Required]
        // 1 ==> پورسانت درصدی
        // 2 ==> پورسانت به ازای هر ثبت نام
        public short CommissionType { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }



        public virtual ICollection<Wallet_Collaborator_CommissionRule> Wallet_Collaborator_CommissionRule_List { get; set; }
    }
}
