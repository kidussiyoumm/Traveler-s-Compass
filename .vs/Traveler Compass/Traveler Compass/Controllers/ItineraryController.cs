using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        [Route("Get")]
        public async Task<ActionResult<List<ItineraryDTO>>> GetAllItineraryAsync()
        {
            var getAll = await _itineraryrepository.GetAllItineraryAsync();
            return Ok();
        }

        [HttpGet]
        [Route("{itineraryId}")]
        public async Task<ActionResult<ItineraryDTO>>GetItineraryByIdAsync(int itineraryId)
        {  
            
            var fetchId = await _itineraryrepository.GetItineraryById(itineraryId);

           
            
            try
            {
              if(fetchId == null)
                {
                    return NotFound();
                }
            
            var response = _mapper.Map<ItineraryDTO>(fetchId);

            return Ok(response);
           
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }



           
        }
    }
}
