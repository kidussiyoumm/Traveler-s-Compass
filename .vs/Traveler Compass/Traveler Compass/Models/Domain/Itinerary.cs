namespace Traveler_Compass.Models.Domain
{
    public class Itinerary
    {
        public int itineraryId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }

        public List<Package> packages { get; set; } //one to many relationship
        public int userId { get; set; } //foreign key for user class 

        public User User { get; set; } // Navigation property for User


        public Itinerary()
        {
            packages = new List<Package>();
        }
    }
}
