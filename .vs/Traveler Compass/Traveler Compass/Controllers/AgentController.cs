using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO;
using Traveler_Compass.Models.DTO.Agent;
using Traveler_Compass.Models.DTO.AgentDto;
using Traveler_Compass.Repository.Implementation;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]

    [ApiController]
    public class AgentController : Controller
    {

        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;
        public AgentController(IAgentRepository _agentRepository, IMapper _mapper)
        {
            this._agentRepository = _agentRepository;
            this._mapper = _mapper;

        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<List<AgentDTO>>> GetAllAgents()
        {
            var agents = await _agentRepository.GetAllAgents();

            var agentDTO = agents.Select( agents=> _mapper.Map<AgentDTO>(agents)).ToList();
            return Ok(agentDTO);
        }


        [HttpPost]
        [Route("post")]
        public async Task<IActionResult>CreateAgent([FromBody] CreateAgentDTO AgentDto)
       
        {
            try
            {
                //Converts the clients data from DTO to our representation of Agent
                var agent = _mapper.Map<Agent>(AgentDto);

                //the DTO frormat mapped to our Agent is not storing into our Database
                //But also this returns the user object that was created and saved
                var createAgent = await _agentRepository.CreateAgentAsync(agent);

                //we've saved the user in the database, the client expects the response to be in the form of a DTO
                //mapper again to convert the created agent object (in domain model format) into a DTO format that the client
                var response = _mapper.Map<AgentDTO>(createAgent);

                return Ok(response);

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error creating user: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //This Method is used to Fetch agent Id from Agent Repository and delete it from the database
        [HttpDelete]
        [Route("{agentId}")]
        public async Task<IActionResult>DeleteAgent(int agentId)
        {
            //no need to map the deleted user to a DTO since the user
            //is no longer available after deletion
            try
            {
                if(agentId == 0)
                {
                    throw new Exception("Agent Doesn't Exist");
                }

                var deleteAgentId = await _agentRepository.GetAgentIdAsync(agentId);

                if(agentId == deleteAgentId.agentId)
                {
                    await _agentRepository.DeleteAgentAsync(agentId);
                }

                return Ok();
            }
            catch(Exception ex) {

                return BadRequest(ex.Message);
                throw;
            }
        }


        [HttpPut]
        [Route("{agentId}")]

        public async Task<IActionResult>UpdateAgentUsingId(int agentId, [FromBody] AgentDTO agentDTO)
        {
            try
            {
                var fetchedId = await _agentRepository.GetAgentIdAsync(agentId);
                if(fetchedId.agentId != agentId)
                {
                    return BadRequest("ID missmatch, please check again");
                }
                if(fetchedId == null)
                {
                    return NotFound();

                }

                var clientRequest = _mapper.Map<Agent>(agentDTO);
                await _agentRepository.UpdateAgentAsync(fetchedId.agentId, clientRequest);
                var response = _mapper.Map<AgentDTO>(clientRequest);

                return Ok(response);
                
            }catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

    }
}
