namespace DbEntities
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string ProvinceName { get; set; }

        [Required]
        public bool Active { get; set; }



        public virtual ICollection<City> CityList { get; set; }
    }
}
