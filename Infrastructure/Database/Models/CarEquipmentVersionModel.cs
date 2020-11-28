using Application.Models;
using Application.Projections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Database.Models
{
    public class CarEquipmentVersionModel : ILanguageContent
    {
        public const string TableName = "CarEquipmentVersion";
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string LangCode { get; set; }
        
        public CarEquipmentProjection ToProjection()
        {
            return new CarEquipmentProjection()
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };
        }
    }
}