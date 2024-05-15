using System.ComponentModel.DataAnnotations;
using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Models.DTO.PacakgeDto
{
    public class PackageDTO
    {

        [Key]
        public int packageId { get; set; }
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }
        public int price { get; set; }



    }
}
