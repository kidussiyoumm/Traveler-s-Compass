namespace Traveler_Compass.Models.Domain
{
    public class Agent
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public long phoneNumber { get; set; }

        public List<Package> packages { get; set; }
        public List<Itinerary> itineraries { get; set; }


        public Agent()
        {
            packages = new List<Package>();
            itineraries = new List<Itinerary>();
        }
    
}
}
