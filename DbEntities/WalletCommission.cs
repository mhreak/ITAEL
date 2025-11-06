namespace DbEntities
{
    [Table(name: "WalletCommission")]
    public class WalletCommission
    {
        [Key]
        public int WalletCommissionId { get; set; }

        [Required]
        public int WalletId { get; set; }

        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; }

        [Required]
        public int Commission { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }
    }
}
