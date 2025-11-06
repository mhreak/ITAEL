namespace DbEntities
{
    [Table("City")]
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string CityName { get; set; }

        [Required]
        public int ProvinceId { get; set; }


        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }


        [Required]
        public bool Active { get; set; }



        public virtual ICollection<Applicant> ApplicantList { get; set; }
    }
}
