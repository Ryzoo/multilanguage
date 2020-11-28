using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Projections;

namespace Application.Interfaces.Database.Repositories
{
    public interface ICarRepository
    {
        Task<ICollection<CarProjection>> GetCarsAsync();
        Task InitializeCarsAsync();
    }
}