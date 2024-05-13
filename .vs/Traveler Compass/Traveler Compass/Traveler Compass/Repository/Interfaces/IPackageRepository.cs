using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{
    public interface IPackageRepository
    {



        Task<Package> CreatePackageAsync(Package package);
        //This takes in user insert it in the database and return the created user
        Task<Package> GetPackageAsyncById(int PackageId);
        Task <List<Package>>GetAllPackage();
        Task<Package> UpdatePackageAsync(int PackageId, Package package);
        Task<Package> DeletePackageAsync(int PackageId);
        Task<Package> GetPackageByNameAsync(string PackageName);


    }  
}
