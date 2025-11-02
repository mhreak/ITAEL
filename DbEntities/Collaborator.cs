using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DbEntities
{
    [Table("Collaborator")]
    public class Collaborator
    {
        [Key]
        public int CollaboratorId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(6)]
        public string ReferralCode { get; set; }

        [Required]
        public bool Active { get; set; }

        public virtual ICollection<Wallet_Collaborator_CommissionRule> Wallet_ReferralCode_CommissionRule_List { get; set; }
    }
}
