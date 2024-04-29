namespace Traveler_Compass.Models.DTO.UserDto
{
    public class UserDTO
    {
        // will have all the info required to the user 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public long phoneNumber { get; set; }
        public char gender { get; set; }
    }
}
