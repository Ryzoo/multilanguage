using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Database.Repositories;
using Application.Interfaces.Services;
using Application.Projections;
using Infrastructure.Database.Models;
using MongoDB.Driver;

namespace Infrastructure.Database.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DatabaseContext _context;
        private readonly IContentLanguageService _languageService;

        public CarRepository(DatabaseContext context, IContentLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }

        public async Task<ICollection<CarProjection>> GetCarsAsync()
        {
            return await _context.Cars
                .Find(_ => true)
                .Project(x => CarModel.ToProjection(x, _languageService))
                .ToListAsync();
        }
        
        public async Task InitializeCarsAsync()
        {
            await _context.Cars
                .DeleteManyAsync(_ => true);

            var cars = new List<CarModel>()
            {
                new CarModel(){Name="Audi", BasePrice = 100, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Full", Description = "Test field", LangCode = "en-US"},
                    new CarEquipmentVersionModel(){Name = "Pełne", Description = "Testowe pole", LangCode = "pl"},
                    new CarEquipmentVersionModel(){Name = "Bla", Description = "bla bla bla", LangCode = "de"},
                }}
            };
            
            await _context.Cars
                .InsertManyAsync(cars);
        }
    }
}