using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Models.DTO.PacakgeDto
{
    public class CreatePackageDTO
    {

    
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }

        public int AgentId { get; set; }


    }
}
