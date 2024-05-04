using Microsoft.AspNetCore.Mvc;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{
    public class ItineraryRepository : IItineraryRepository
    {
        //The repository class is where the database is accessed using the interface class. 
        //It will be called by the controller class, we in return save and store the data here 
        //but we have logic in the controller class.
        private readonly CompassDbContext dbContext;
        public ItineraryRepository(CompassDbContext dbContext) 
        {
            this.dbContext = dbContext; 
        }
        public async Task<Itinerary> CreateItineraryAsync(Itinerary itinerary)
        {
            await dbContext.itineraries.AddAsync(itinerary); //User table beign populated 
            await dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return itinerary;
        }

        public async Task<Itinerary> DeleteItineraryAsync(int itineraryId)
        {
            var selectedItinerary = await dbContext.itineraries.FindAsync(itineraryId);
            if (selectedItinerary == null)
            {
                return null;
            }
            dbContext.itineraries.Remove(selectedItinerary);
            await dbContext.SaveChangesAsync();

            return selectedItinerary;



        }

        public IEnumerable<Itinerary> GetAllItinerary()
        {
            throw new NotImplementedException();
        }

        public Task<Itinerary> UpdateItineraryAsync(int itineraryid, Itinerary itinerary)
        {
            throw new NotImplementedException();
        }
    }
}
