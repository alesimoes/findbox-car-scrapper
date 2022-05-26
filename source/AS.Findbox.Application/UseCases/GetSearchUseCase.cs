using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.UseCases
{
    public class GetSearchUseCase
    {
        private ICarScraperService _service;

        public GetSearchUseCase(ICarScraperService service)
        {
            _service = service;
        }

        public async Task<List<CarResponse>> Execute(GetSearchRequest request)
        {
            var cars = await _service.GetCars(request.Make, request.Model);
            return cars;
        }
    }
}
