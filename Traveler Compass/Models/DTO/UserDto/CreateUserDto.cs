using System.ComponentModel.DataAnnotations;

namespace Traveler_Compass.Models.DTO.UserDto
{
    public class CreateUserDto
    {
        [Required]
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

        public string password { get; set; }
        public long phoneNumber { get; set; }

        [Required]

        public char gender { get; set; }
    }
}
