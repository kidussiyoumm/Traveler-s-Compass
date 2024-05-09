﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Traveler_Compass.Models.Domain
{
    public class Agent
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

        
        public List<Package> packages { get; set; } 
        public List<Itinerary> itineraries { get; set; }


        public Agent()
        {
            packages = new List<Package>();
            itineraries = new List<Itinerary>();
        }
    
}
}
