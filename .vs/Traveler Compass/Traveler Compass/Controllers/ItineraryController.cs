using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.ItineraryDto;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Controllers
{
    public class ItineraryController : Controller
    {


        private readonly IItineraryRepository _itineraryrepository;
        private readonly IMapper _mapper;
        public ItineraryController(IItineraryRepository _itineraryRepository, IMapper _mapper)
        {

            this._itineraryrepository = _itineraryRepository;
            this._mapper = _mapper;
        }


        [HttpGet]
        [Route("api/Itinerary/GetAllItineraryAsync")]
        public async Task<ActionResult<List<ItineraryDTO>>> GetAllItineraryAsync()
        {
            var getAll = await _itineraryrepository.GetAllItineraryAsync();
            return Ok(getAll);
        }

        [HttpGet]
        [Route("api/Itinerary/{itineraryId}/GetItineraryByIdAsync")]
        public async Task<ActionResult<ItineraryDTO>> GetItineraryByIdAsync(int itineraryId)
        {

            var fetchId = await _itineraryrepository.GetItineraryById(itineraryId);

            try
            {
                if (fetchId == null)
                {
                    return NotFound();
                }

                var response = _mapper.Map<ItineraryDTO>(fetchId);

                return Ok(response);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [Route("api/Itinerary/{itineraryName}/GetItineraryByNameAsync")]
        public async Task<ActionResult<ItineraryDTO>> GetItineraryByNameAsync(string itineraryName)
        {
            try
            {
                if (string.IsNullOrEmpty(itineraryName))
                {
                    return BadRequest("Itinerary Title is required");
                }

                var fetchName = await _itineraryrepository.GetItineraryByName(itineraryName);


                if (fetchName != null)
                {
                    var response = _mapper.Map<ItineraryDTO>(fetchName);
                    return Ok(response);
                }
                else
                {
                    return NotFound($"{itineraryName} not found");
                }




            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/Itinerary/{itineraryId}/DeleteItinerary")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Itinerary>> DeleteItineraryAsync(int itineraryId)
        {
            var fetchId = await _itineraryrepository.GetItineraryById(itineraryId);
            try
            {
                if (fetchId != null)
                {
                    await _itineraryrepository.DeleteItineraryAsync(fetchId.itineraryId);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpPost]
        [Route("api/Itinerary/{itineraryId}/UpdateItineraries")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateItineraryAsync(int itineraryId, [FromBody] ItineraryDTO itineraryDTO)
        {
            var fetchId = await _itineraryrepository.GetItineraryById(itineraryId);

            try
            {
                if (fetchId.userId != itineraryId)
                {
                    return BadRequest("ID mismatch between URL and user data.");

                }
                if (fetchId == null)
                {
                    return NotFound();
                }

                var clinetData = _mapper.Map<Itinerary>(itineraryDTO);
                var updateDatabase = await _itineraryrepository.UpdateItineraryAsync(itineraryId, clinetData);
                var response = _mapper.Map<ItineraryDTO>(updateDatabase);

                return Ok(response);

            }   
            catch(Exception ex) {
               
                Console.WriteLine(ex.Message);
                return StatusCode(500);
           
            }
      
        }

        [HttpPut]
        [Route("api/Itinerary/{itineraryId}/UpdateItineraryById")]
        [Authorize(Roles = "Admin")]
        public async Task <ActionResult<ItineraryDTO>> UpdateItineraryByIdAsync(int itineraryId, [FromBody] ItineraryDTO itineraryDto)
        {
            try { 
            var fetchItinerary = await _itineraryrepository.GetItineraryById(itineraryId);

            if (fetchItinerary != null)
             {
                    var clientData = _mapper.Map<Itinerary>(itineraryDto);
                    var databaseFetch = await _itineraryrepository.UpdateItineraryAsync(itineraryId, clientData);
                    var response = _mapper.Map<ItineraryDTO>(databaseFetch);

                    return Ok(response);
                }
                else
                {
                    return BadRequest("update was unsuccesful, Try again");

                }
                  
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        
    }
}
