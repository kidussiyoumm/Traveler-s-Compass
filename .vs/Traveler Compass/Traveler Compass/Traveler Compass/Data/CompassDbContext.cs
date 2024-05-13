using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Data
{
    public class CompassDbContext : DbContext
    {
        public CompassDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Package> packages { get; set; }
        public DbSet<Itinerary> itineraries { get; set; }
        public DbSet<Agent> agents { get; set; }
        //public DbSet<Role> roles { get; set; } 

    }
}
