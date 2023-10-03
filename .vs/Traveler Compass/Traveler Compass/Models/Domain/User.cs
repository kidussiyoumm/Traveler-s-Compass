namespace Traveler_Compass.Models.Domain
{
    public class User
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public long phoneNumber { get; set; }
        public char gender { get; set; }

        public List<Package> packages { get; set; } //one to many relationship
        public List<Itinerary> itineraries { get; set; } //one to many relationship


        public User()
        {
            packages = new List<Package>();
            itineraries = new List<Itinerary>();

        }

    }
}
