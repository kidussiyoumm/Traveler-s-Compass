using System.ComponentModel.DataAnnotations;

namespace Traveler_Compass.Models.DTO.Agent
{
    public class AgentDTO
    {
        public string agentFristName { get; set; }
        public string agentLastName { get; set; }
        public string companyName { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string agentGender { get; set; }
        public long phoneNumber { get; set; }

    }
}
