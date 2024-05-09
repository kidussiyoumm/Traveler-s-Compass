using Microsoft.EntityFrameworkCore;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{
    public class PackageRepository : IPackageRepository
    {

        //we will never deal with the DTOs inside the repositiories, thats for the controller

        private readonly CompassDbContext _dbContext;

        public PackageRepository(CompassDbContext _dbContext) //DI from compassDbContext class
        {
            this._dbContext = _dbContext;
        }

        //To get a user from DB
        public async Task<List<Package>>GetAllPackage()
        {
            return await _dbContext.packages.ToListAsync();
        }

        //To create a user
        public async Task<Package> CreatePackageAsync(Package package) //we will call this in the controller class 
        {
            await _dbContext.packages.AddAsync(package); //Package table beign populated 
            await _dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return package;
        }

        public async Task<Package> GetPackageAsyncById(int packageId)
        {
            
            var fetchData = await _dbContext.packages.FirstOrDefaultAsync(x => x.packageId == packageId);
            try
            {
  
                if(fetchData == null)
                {
                   throw new Exception($"{packageId} is Null, Try again");
                }
    
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return fetchData;

        }

        public async Task<Package> UpdatePackageAsync(int PackageId, Package package)
        {
            var existingPackage = _dbContext.packages.FirstOrDefault(x => x.packageId == PackageId);
            
            try
            {
                if(existingPackage != null)
                {
                    existingPackage.title = package.title;
                    existingPackage.description = package.description;
                    existingPackage.price = package.price;
                    
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"{existingPackage} Is null, Try again");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return existingPackage;
            
        }

        public async Task<Package> DeletePackageAsync(int PackageId)
        {
            var fetchData = await _dbContext.packages.FindAsync(PackageId);
            try
            {
                if (fetchData == null)
                {
                    throw new Exception($"{fetchData} is null");
                }
                _dbContext.packages.Remove(fetchData);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return fetchData;
        }

        public async Task<Package> GetPackageByNameAsync(string PackageName)
        {
            var namePackage = await _dbContext.packages.FirstOrDefaultAsync(x => x.title == PackageName);

            try
            {
                if(namePackage == null)
                {
                    throw new Exception($"{namePackage} is wrong Entry, Try again");
                }

                return namePackage;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
