using AS.Findbox.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Domain.Cars
{
    public class CarService : ICarService
    {
        private ICarRepository _repository;

        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task Save(Car car)
        {
            var existingCar = await _repository.Find(car.Id);
            if(existingCar != null)
            {
                 await _repository.Update(car);
            }
            else
            {
                await _repository.Add(car);
            }
        }

    }
}
