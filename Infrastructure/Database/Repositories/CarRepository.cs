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
                .Project(x => x.ToProjection(_languageService))
                .ToListAsync();
        }
        
        public async Task<CarProjection> GetCarByIdAsync(string id)
        {
            return await _context.Cars
                .Find(x => x.Id == id)
                .Project(x => x.ToProjection(_languageService))
                .FirstOrDefaultAsync();
        }
        
        public async Task InitializeCarsAsync()
        {
            await _context.Cars
                .DeleteManyAsync(_ => true);

            var cars = new List<CarModel>()
            {
                new CarModel(){Name="Opel - en, pl, de", BasePrice = 100, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Full", Description = "Test field", LangCode = "en"},
                    new CarEquipmentVersionModel(){Name = "Pełne", Description = "Testowe pole", LangCode = "pl"},
                    new CarEquipmentVersionModel(){Name = "Vollständig", Description = "Testfeld", LangCode = "de"},
                }},
                new CarModel(){Name="Audi - pl, de", BasePrice = 200, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Pełne", Description = "Testowe pole", LangCode = "pl"},
                    new CarEquipmentVersionModel(){Name = "Vollständig", Description = "Testfeld", LangCode = "de"},
                }},
                new CarModel(){Name="BMW - pl", BasePrice = 200, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Pełne", Description = "Testowe pole", LangCode = "pl"},
                }},
                new CarModel(){Name="Skoda - ru", BasePrice = 200, CarEquipmentVersion = new List<CarEquipmentVersionModel>()
                {
                    new CarEquipmentVersionModel(){Name = "Полный текст", Description = "Испытательное поле", LangCode = "ru"},
                }},
                new CarModel(){Name="Other", BasePrice = 200}
            };
            
            await _context.Cars
                .InsertManyAsync(cars);
        }
    }
}