using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{
    public class PackageRepository : IPackageRepository
    {

        //we will never deal with the DTOs inside the repositiories, thats for the controller

        private readonly CompassDbContext dbContext;

        public PackageRepository(CompassDbContext dbContext) //DI from compassDbContext class
        {
            this.dbContext = dbContext;
        }

        //To get a user from DB
        public IEnumerable<Package> GetAllPackage()
        {
            return dbContext.packages.ToList();
        }

        //To create a user
        public async Task<Package> CreatePackageAsync(Package package) //we will call this in the controller class 
        {
            await dbContext.packages.AddAsync(package); //Package table beign populated 
            await dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return package;
        }

        public Task<Package> GetPackageAsync(int PackageId)
        {
            throw new NotImplementedException();
        }

        public Task<Package> UpdatePackageAsync(int PackageId, Package package)
        {
            throw new NotImplementedException();
        }

        public Task<Package> DeletePackageAsync(int PackageId)
        {
            throw new NotImplementedException();
        }
    }
}
