using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Traveler_Compass.Models.Domain
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [StringLength(50)]
        public string firstName { get; set; }
        [Required]
        [StringLength(50)]
        public string lastName { get; set; }

        [DataType(DataType.EmailAddress)] 
        public string email { get; set; }
       
        public string password{ get; set; }
        public long phoneNumber { get; set; }

        [Required]
        public char gender { get; set; }
      
        public List<Package> packages { get; set; } //one to many relationship
      
        public List<Itinerary> itineraries { get; set; } //one to many relationship

        public bool isAgent { get; set; }

        public User()
        {
            packages = new List<Package>();
            itineraries = new List<Itinerary>();

        }

    }
}
