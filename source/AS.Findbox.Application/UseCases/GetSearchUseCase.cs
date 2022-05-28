using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Domain.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.UseCases
{
    public class GetSearchUseCase
    {
        private ICarScraperService _carScraperService;
        private ICarService _service;

        public GetSearchUseCase(ICarService service, ICarScraperService carScraperService )
        {
            _carScraperService = carScraperService;
            _service = service;
        }

        public async Task<List<CarResponse>> Execute(GetSearchRequest request)
        {
            var cars = await _carScraperService.GetCars(request.Make, request.Model);

            foreach (var car in cars)
            {
                await _service.Save(new Car(car.Id, car.Title, car.Make, car.Model, car.Condition, car.Mileage, car.Value, car.Rating, car.Picture));
            }

            return cars;
        }
    }
}
