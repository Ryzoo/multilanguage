using System.Collections.Generic;
using Application.Interfaces.Services;
using Application.Projections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Database.Models
{
    public class CarModel
    {
        public const string TableName = "Car";
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public ICollection<CarEquipmentVersionModel> CarEquipmentVersion { get; set; }

        public static CarProjection ToProjection(CarModel model, IContentLanguageService languageService)
        {
            return new CarProjection()
            {
                Id = model.Id,
                Name = model.Name,
                BasePrice = model.BasePrice,
                CarEquipmentVersion = languageService
                    .PrepareContent(model.CarEquipmentVersion)
                    .ToProjection()
            };
        }
    }
}