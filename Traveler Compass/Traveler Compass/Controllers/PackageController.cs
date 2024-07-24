using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.PacakgeDto;
using Traveler_Compass.Repository.Implementation;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IAgentRepository _agentRespostory;
        private readonly IMapper _mapper;
        public PackageController(IPackageRepository _packageRepository,
                                 IAgentRepository _agentRespostory,
                                  IMapper _mapper)
        { //Any connection to the data base should be through the repository not directly to the Data base


            this._packageRepository = _packageRepository;
            this._agentRespostory = _agentRespostory;
            this._mapper = _mapper; 

        }
        [HttpGet]
        [Route("api/package/GetAllPackageAsync")]
        public async Task<ActionResult<List<Package>>> GetAllPackageAsync()
        {
            var package = await _packageRepository.GetAllPackage();
            return Ok(package);
        }
        [HttpGet]
        [Route("api/package/{packageId}/GetPackageByIdAsync")]
        public async Task<ActionResult> GetPackageByIdAsync(int packageId)
        {
            
            try
            {
                var fetchId = await _packageRepository.GetPackageAsyncById(packageId);
                if(fetchId == null)
                {
                    throw new Exception("{fetchId} not Found");
                }
                return Ok(fetchId); 

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpGet]
        [Route("api/package/{packageTitle}/GetPackageByTitleAsync")]
        public async Task<IActionResult> GetPackageByTitleAsync(string packageTitle)
        {
            try
            {
                if (string.IsNullOrEmpty(packageTitle))
                {
                    return NotFound();
                }

               var response = await _packageRepository.GetPackageByNameAsync(packageTitle);
               return Ok(response);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Route("api/package/CreatePackageAsync")]
     // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PackageDTO>> CreatePackageAsync([FromBody] CreatePackageDTO packageDto)
        {
            try
            {
                // Validate if the AgentId exists
                var agentExists = await _agentRespostory.GetAgentIdAsync(packageDto.AgentId);
                if (agentExists == null)
                {
                    return BadRequest("Invalid AgentId.");
                }

                var ClientData =  _mapper.Map<Package>(packageDto);
                // Set UserId to null or default if not applicable
                ClientData.userId = null;
                var createdPackage = await _packageRepository.CreatePackageAsync(ClientData);
                var response = _mapper.Map<PackageDTO>(createdPackage);
               
                return Ok(response);


            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
                throw;
            }
                     
        }

        [HttpPut]
        [Route("api/package/UpdatePackageAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PackageDTO>> UpdatePackageAsync(int packageId, [FromBody] CreatePackageDTO packageDTO)
        { 
            
            var fetchData = await _packageRepository.GetPackageAsyncById(packageId);

            try
                {
                 if(fetchData.packageId != packageId)
                {
                    return NotFound($"{packageId} can not be found");
                }
                 if(fetchData == null)
                {
                    return NotFound($"{packageId} can not be found");
                }

                    var clientData = _mapper.Map<Package>(packageDTO);
                    var updateDataBase = await _packageRepository.UpdatePackageAsync(packageId, clientData);
                    var response = _mapper.Map<PackageDTO>(updateDataBase);
                     return Ok(response);
                
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        [HttpDelete]
        [Route("api/package/{packageId}/DeletepackageByIdAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletepackageByIdAsync(int packageId)
        {
            var fetchId = await _packageRepository.GetPackageAsyncById(packageId);
            try
            {
                if(fetchId.packageId != packageId)
                {
                    return BadRequest();
                }
                var deletePacakge = _packageRepository.DeletePackageAsync(fetchId.packageId);
                return Ok(deletePacakge);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
