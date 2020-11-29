using Application.Interfaces.Database.Models;
using Application.Projections;

namespace Infrastructure.Database.Models
{
    public class CarEquipmentVersionModel : ILanguageContent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        
        public CarEquipmentProjection ToProjection()
        {
            return new CarEquipmentProjection()
            {
                Name = Name,
                Description = Description,
            };
        }
    }
}