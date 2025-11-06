namespace DbEntities
{
    [Table("Wallet")]
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        [Required]
        [MaxLength(50)]
        public string WalletName { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }



        public virtual ICollection<WalletCommission> WalletCommissionList { get; set; }
        public virtual ICollection<Wallet_Collaborator_CommissionRule> Wallet_Collaborator_CommissionRule_List { get; set; }
    }
}
