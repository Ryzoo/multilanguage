using System.Collections.Generic;

namespace Application.Projections
{
    public class CarProjection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public CarEquipmentProjection CarEquipmentVersion { get; set; }
    }
}