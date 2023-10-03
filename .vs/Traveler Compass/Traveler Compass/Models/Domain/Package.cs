namespace Traveler_Compass.Models.Domain
{
    public class Package
    {
        public int packageId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }

        public int userId { get; set; } //foreign key for user class 
        public User user { get; set; } // Navigation property for User

        public int agentId { get; set; }
        public Agent agent { get; set; }

        public int itineraryId { get; set; }
        public Itinerary itinerary { get; set; }


    }
}
