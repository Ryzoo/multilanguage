using Application.Settings;
using Infrastructure.Database.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Database
{
    public class DatabaseContext
    {
        public IMongoCollection<CarModel> Cars { get; }

        public DatabaseContext(IOptions<DbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);
            
            Cars = database.GetCollection<CarModel>(CarModel.TableName);
        }
    }
}