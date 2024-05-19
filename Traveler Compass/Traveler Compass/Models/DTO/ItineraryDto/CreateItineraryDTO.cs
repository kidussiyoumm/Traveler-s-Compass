using System.ComponentModel.DataAnnotations;

namespace Traveler_Compass.Models.DTO.ItineraryDto
{
    public class CreateItineraryDTO
    {
        [Key]
        public int itineraryId { get; set; }
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }
        public int price { get; set; }

    }
}
