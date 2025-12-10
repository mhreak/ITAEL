namespace DbEntities
{
    public class ContactMessage
    {
        [Key]
        public int ContactMessageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(11)]
        public string Mobile { get; set; }

        [MaxLength(4000)]
        [Required]
        public string Text { get; set; }
    }
}
