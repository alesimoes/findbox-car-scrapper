using As.Findbox.Repository.MongoDB;
using AS.Findbox.Domain.Cars;
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
    }
}
