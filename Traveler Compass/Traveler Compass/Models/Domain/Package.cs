using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Traveler_Compass.Models.Domain
{
    public class Package
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

        [ForeignKey("UserId")]
        public int? userId { get; set; } //foreign key for user class 
        public User user { get; set; } // Navigation property for User

        [ForeignKey("AgentId")]
        public int agentId { get; set; }
        public Agent agent { get; set; }

       
        public ICollection<Itinerary> Itineraries { get; set; }

      public Package()
        {
            Itineraries = new List<Itinerary>();
        }


    }
}
