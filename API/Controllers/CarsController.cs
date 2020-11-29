using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Application.Interfaces.Database.Repositories;
using Application.Projections;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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
        
        [HttpPost]
        public async Task InitializeCars()
        {
            await _carRepository.InitializeCarsAsync();
        }
    }
}