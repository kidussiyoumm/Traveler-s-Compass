using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Traveler_Compass.Models.Domain; 
//using Traveler_Compass.Helper.ICreateJWT;

namespace Traveler_Compass.Helper
{
    public class JwtTokenUtility : ICreateJWT
    {
        private readonly IConfiguration _configuration;
        public JwtTokenUtility(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public string CreateJWT(User user)
        {

            //using SymerticKey
           
            var SecurityKey = _configuration.GetSection("Jwt:key").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                        GetBytes(SecurityKey));

            //claims as an array(multiple) bits of infromation from user 
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim("customClaimType", "customClaimValue")

            };

            //we need signin creditails to define the secert key and algorithm
            var SigningCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);//the bigger the number the bigger the key is

            //we need a secuirty token discreptor this will use all the above variables
            //TO define all the infomation to generate the token
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), //takes in the claim
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = SigningCredentials

            };

            //Token Hanlder response to handle jwt
            var tokenHanlder = new JwtSecurityTokenHandler();
            var token = tokenHanlder.CreateToken(tokenDesciptor); // we pass the toekn description with all the  
                                                                  // required infomation to generate a token
            return tokenHanlder.WriteToken(token);//return Token
        }

        public string CreateJWTAgent(Agent agent)
        {
            //using SymerticKey

            var SecurityKey = _configuration.GetSection("Jwt:key").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                        GetBytes(SecurityKey));

            //claims as an array(multiple) bits of infromation from user 
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, agent.email),
                new Claim(ClaimTypes.NameIdentifier, agent.agentId.ToString()),
                new Claim("customClaimType", "customClaimValue")

            };

            //we need signin creditails to define the secert key and algorithm
            var SigningCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);//the bigger the number the bigger the key is

            //we need a secuirty token discreptor this will use all the above variables
            //TO define all the infomation to generate the token
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), //takes in the claim
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = SigningCredentials

            };

            //Token Hanlder response to handle jwt
            var tokenHanlder = new JwtSecurityTokenHandler();
            var token = tokenHanlder.CreateToken(tokenDesciptor); // we pass the toekn description with all the  
                                                                  // required infomation to generate a token
            return tokenHanlder.WriteToken(token);//return Token
        }
    }
}
