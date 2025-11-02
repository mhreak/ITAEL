using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("Setting")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SettingId { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        [Required]
        public string SettingName { get; set; }

        [MaxLength(1500)]
        [MinLength(2)]
        [Required]
        public string SettingKey { get; set; }

        [MaxLength(1500)]
        public string SettingValue { get; set; }
    }
}
