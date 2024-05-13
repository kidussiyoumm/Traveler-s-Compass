using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.Agent;

using Traveler_Compass.Models.DTO.UserDto;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IRegisterRepository _registerRepository;
        private readonly IMapper _mapper;

        public RegisterController(IRegisterRepository _registerRepository, IMapper _mapper)
        {
            this._registerRepository = _registerRepository;
            this._mapper = _mapper;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDto, bool isAgent)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var registeredUser = await _registerRepository.RegisterUserAsync(user, isAgent);
                if (isAgent)
                {
                   var response = _mapper.Map<AgentDTO>(registeredUser);
                    return Ok(response);
                }
                var userResponse = _mapper.Map<UserDTO>(registeredUser);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("register-agent")]
        public async Task<IActionResult> RegisterAgent([FromBody] AgentDTO agentDto)
        {
            try
            {
                var agent = _mapper.Map<Agent>(agentDto);
                var registeredAgent = await _registerRepository.RegisterAgentAsync(agent);
                var response = _mapper.Map<AgentDTO>(registeredAgent);

                return Ok(registeredAgent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}