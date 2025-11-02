using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("Wallet_ReferralCode_CommissionRule")]
    public class Wallet_ReferralCode_CommissionRule
    {
        [Key]
        [Column(Order = 0)]
        public int WalletId { get; set; }

        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ReferralCodeId { get; set; }

        [ForeignKey("ReferralCodeId")]
        public virtual ReferralCode ReferralCode { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CommissionRuleId { get; set; }

        [ForeignKey("CommissionRuleId")]
        public virtual CommissionRule CommissionRule { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
