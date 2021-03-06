using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Domain.Cars
{
    public interface ICarRepository
    {
        Task Add(Car car);
        Task<Car> Find(Guid id);
        Task Update(Car car);
    }
}
