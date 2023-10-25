using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Traveler_Compass.Models.Domain
{
    public class Itinerary
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

        // public List<Package> packages { get; set; } //one to many relationship

        [ForeignKey("User")]
        public int userId { get; set; } //foreign key for user class 
        public User User { get; set; } // Navigation property for User


        //public Itinerary()
        //{
        //    packages = new List<Package>();
        //}
    }
}
