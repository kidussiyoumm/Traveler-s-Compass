using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Traveler_Compass.Helper;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.LoginDto;
using Traveler_Compass.Models.DTO.UserDto;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Account controller
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly ICreateJWT _createJWT;
        public AccountController(IUserRepository _userRepository,
                                 IAgentRepository _agentRepository,
                                 ICreateJWT _createJWT)
        {
            this._userRepository = _userRepository;
            this._agentRepository = _agentRepository;
            this._createJWT = _createJWT;

        }

        [HttpPost]
        [Route("login/User")]
        public async Task<IActionResult> Login([FromBody] LoginReqDTO loginReq)
        {
            try
            {
                if (string.IsNullOrEmpty(loginReq.email) || string.IsNullOrEmpty(loginReq.password))
                {
                    return BadRequest("Email and password is Required");
                }
                var fetchUser = await _userRepository.AuthenticateUser(loginReq.email, loginReq.password);
                if (fetchUser == null)
                {
                    return Unauthorized();
                }

                var loginRes = new LoginResDTO();
                loginRes.email = fetchUser.email;
                loginRes.token = _createJWT.CreateJWT(fetchUser);


                return Ok(loginRes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }

        }

        [HttpPost]
        [Route("login/AsAgent")]
        public async Task<IActionResult> LoginAgent([FromBody] LoginReqDTO loginReq)
        {
            try
            {
                if (string.IsNullOrEmpty(loginReq.email) || string.IsNullOrEmpty(loginReq.password))
                {
                    return BadRequest("Email and password is Required");
                }
                var fetchUser = await _agentRepository.AuthenticateAgent(loginReq.email, loginReq.password);
                if (fetchUser == null)
                {
                    return Unauthorized();
                }

                var loginRes = new LoginResDTO();
                loginRes.email = fetchUser.email;
                loginRes.token = _createJWT.CreateJWTAgent(fetchUser);


                return Ok(loginRes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }

        }




    }
}
