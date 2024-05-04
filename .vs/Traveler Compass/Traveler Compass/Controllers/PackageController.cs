using Microsoft.AspNetCore.Mvc;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.PacakgeDto;
using Traveler_Compass.Repository.Implementation;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository packageRepository;
        public PackageController(IPackageRepository packageRepository)
        { //Any connection to the data base should be through the repository not directly to the Data base


            this.packageRepository = packageRepository;
        }
        [HttpGet]

        public ActionResult<IEnumerable<Package>> GetPackage()
        {
            var package = packageRepository.GetAllPackage();
            return Ok(package);
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> CreatePackage([FromBody] CreatePackageDTO packageDto)
        {

            //Map Dto to domain model
            //So that the clients can't have access to certain field such as PacakageId
            var package = new Package //this the Pacakge class
            {

              //  packageId = packageDto.packageId,
                title = packageDto.title,
                description = packageDto.description,
                price = packageDto.price

             };

            await packageRepository.CreatePackageAsync(package); //take in the user and pass it to the UserRepo

            //Map back from model to Dto
            var response = new PackageDTO
            {
             //   packageId = packageDto.packageId,
                title = packageDto.title,
                description = packageDto.description,
                price = packageDto.price

            };
            return Ok(response); // not to use "user" directly its bad practice 
        }

    }
}
