using System.ComponentModel.DataAnnotations;

namespace Traveler_Compass.Models.DTO.AgentDto
{
    public class CreateAgentDTO
    {
        [Key]
        [Required]
        public int agentId { get; set; }

        [Required]
        [StringLength(50)]
        public string agentFristName { get; set; }
        [Required]
        [StringLength(50)]
        public string agentLastName { get; set; }
        [Required]
        [StringLength(50)]
        public string companyName { get; set; }
        public string description { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public long phoneNumber { get; set; }

    }
}
