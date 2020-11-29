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

        public CarProjection ToProjection(IContentLanguageService languageService)
        {
            return new CarProjection()
            {
                Id = Id,
                Name = Name,
                BasePrice = BasePrice,
                CarEquipmentVersion = languageService
                    .PrepareContent(CarEquipmentVersion)
                    ?.ToProjection()
            };
        }
    }
}