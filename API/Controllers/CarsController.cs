using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Database.Repositories;
using Application.Projections;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        [HttpGet]
        public async Task<ICollection<CarProjection>> GetCars()
        {
            return await _carRepository.GetCarsAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<CarProjection> GetCars(string id)
        {
            return await _carRepository.GetCarByIdAsync(id);
        }
        
        [HttpPost]
        public async Task InitializeCars()
        {
            await _carRepository.InitializeCarsAsync();
        }
    }
}