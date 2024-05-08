using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{
    public interface IItineraryRepository
    {

        //This takes in user insert it in the database and return the created user
        Task<Itinerary> CreateItineraryAsync(Itinerary itinerary);
        Task<List<Itinerary>>GetAllItineraryAsync();
        Task<Itinerary> GetItineraryById(int itineraryId);
        Task<Itinerary> GetItineraryByName(string name);
        Task<Itinerary> UpdateItineraryAsync(int itineraryid, Itinerary itinerary);
        Task<Itinerary> DeleteItineraryAsync(int itineraryid);
    }
}
