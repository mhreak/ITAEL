using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("ReferralCode")]
    public class ReferralCode
    {
        [Key]
        public int ReferralCodeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ReferralCodeName { get; set; }

        [Required]
        [MaxLength(30)]
        public string RefCode { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }



        public virtual ICollection<Wallet_ReferralCode_CommissionRule> Wallet_ReferralCode_CommissionRule_List { get; set; }
    }
}
