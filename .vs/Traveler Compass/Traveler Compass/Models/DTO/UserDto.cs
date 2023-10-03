namespace Traveler_Compass.Models.DTO
{
    public class UserDto
    {// will have all the info required to the user 


        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public long phoneNumber { get; set; }
        public char gender { get; set; }
    }
}
