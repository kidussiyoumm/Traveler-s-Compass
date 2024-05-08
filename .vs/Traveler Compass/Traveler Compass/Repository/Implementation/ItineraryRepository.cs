using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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
        private readonly CompassDbContext _dbContext;
        public ItineraryRepository(CompassDbContext _dbContext) 
        {
            this._dbContext = _dbContext; 
        }
         public async Task<List<Itinerary>>GetAllItineraryAsync()
        {
            try 
            {
                return await _dbContext.itineraries.ToListAsync(); 
            
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

          public async Task<Itinerary> GetItineraryById(int itineraryId)
        { 
            var fetchedId = await _dbContext.itineraries.FindAsync(itineraryId);
            try
            {
               
                if (fetchedId == null)
                {
                    throw new Exception($"{fetchedId} was Null");

                }
                

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception($"There was an error found {ex.Message}");
            }

            return fetchedId;
        }


        public Task<Itinerary> GetItineraryByName(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<Itinerary> CreateItineraryAsync(Itinerary itinerary)
        {
            await _dbContext.itineraries.AddAsync(itinerary); //User table beign populated 
            await _dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return itinerary;
        }

        public async Task<Itinerary> DeleteItineraryAsync(int itineraryId)
        {
            var selectedItinerary = await _dbContext.itineraries.FindAsync(itineraryId);
            try
            {
               
                if (selectedItinerary == null)
                {
                    throw new Exception($"Itinerary {itineraryId} can not be found");
                }
                _dbContext.itineraries.Remove(selectedItinerary);
                await _dbContext.SaveChangesAsync();

                
            }catch(Exception ex)
            {
                throw new Exception($"Error has been found {ex.Message}");

            }
                
            return selectedItinerary;

        }

       

        public async Task<Itinerary> UpdateItineraryAsync(int itineraryid, Itinerary UpdatedItinerary)
        {
            var existingItinerary = await _dbContext.itineraries.FindAsync(itineraryid);
            try
            {
                if(existingItinerary != null)
                {
                    existingItinerary.price = UpdatedItinerary.price;
                    existingItinerary.title= UpdatedItinerary.title;
                    existingItinerary.description = UpdatedItinerary.description;

                }
                else
                {
                    throw new Exception($"{existingItinerary} is null");
                }

                return existingItinerary;
            }
            catch(Exception ex) 
            {
                throw new Exception($"Update unsuccessful{ex.Message}");
            }

        }

      
    }
}
