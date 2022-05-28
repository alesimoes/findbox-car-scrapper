using As.Findbox.Repository.MongoDB;
using AS.Findbox.Domain.Cars;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Repository.MongoDB
{
    public class CarRespository : ICarRepository
    {
        private readonly MongoDbContext _context;

        public CarRespository(MongoDbContext context)
        {
            this._context = context;
        }
        public async Task Add(Car car)
        {
            await _context.Cars.InsertOneAsync(car);
        }

        public async Task Update(Car car)
        {
            var updateDefinition = Builders<Car>.Update
           .Set(u => u.AdTitle, car.AdTitle)
           .Set(u => u.Picture, car.Picture)
           .Set(u => u.Value, car.Value)
           .Set(u => u.Mileage, car.Mileage)
           .Set(u => u.Rating, car.Rating);
          
            await _context.Cars.UpdateOneAsync(c=>c.Id == car.Id, updateDefinition);
        }

        public async Task<Car> Find(Guid id)
        {
            return (await _context.Cars.FindAsync(f => f.Id == id)).FirstOrDefault();
        }
    }
}
