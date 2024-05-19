using System.ComponentModel.DataAnnotations;

namespace Traveler_Compass.Models.DTO.UserDto
{
    public class CreateUserDto
    {
      
        [Required]
        [StringLength(50)]
        public string firstName { get; set; }
        [Required]
        [StringLength(50)]
        public string lastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public long? phoneNumber { get; set; }
        public string gender { get; set; }
    }
}
