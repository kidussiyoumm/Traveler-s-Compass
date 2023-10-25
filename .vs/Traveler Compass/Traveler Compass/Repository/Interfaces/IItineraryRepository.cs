using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{
    public interface IItineraryRepository
    {

        Task<Itinerary> CreateItineraryAsync(Itinerary itinerary);//This takes in user insert it in the database and return the created user
        //Task<User> GetUserAsync(string username);
        IEnumerable<Itinerary> GetAllItinerary();
        Task<Itinerary> UpdateItineraryAsync(int itineraryid, Itinerary itinerary);
        Task<Itinerary> DeleteItineraryAsync(int itineraryid);
    }
}
