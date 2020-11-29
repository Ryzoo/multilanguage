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
                new CarModel(){Name="Opel - en,pl,de", BasePrice = 100, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Full", Description = "Test field", LangCode = "en"},
                    new CarEquipmentVersionModel(){Name = "Pełne", Description = "Testowe pole", LangCode = "pl"},
                    new CarEquipmentVersionModel(){Name = "Bla", Description = "bla bla bla", LangCode = "de"},
                }},
                new CarModel(){Name="Audi - only de", BasePrice = 200, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Bla", Description = "bla bla bla", LangCode = "de"},
                }},
                new CarModel(){Name="BMW - only pl", BasePrice = 200, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Pełne", Description = "Testowe pole", LangCode = "pl"},
                }},
                new CarModel(){Name="Skoda - only ze", BasePrice = 200, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Zeee", Description = "Zeeeeee", LangCode = "ze"},
                }},
                new CarModel(){Name="Other", BasePrice = 200}
            };
            
            await _context.Cars
                .InsertManyAsync(cars);
        }
    }
}