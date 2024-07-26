using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Helper
{
    public interface ICreateJWT
    {
        public string CreateJWT(User user);
        public string CreateJWTAgent(Agent usagenter);
    }
}
