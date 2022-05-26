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
    public class GetModelsUseCase
    {
        private ICarScraperService _service;

        public GetModelsUseCase(ICarScraperService service)
        {
            _service = service;
        }

        public async Task<List<ModelResponse>> Execute(GetModelsRequest request)
        {
            var makers = await _service.GetModels(request.Make);
            return makers;
        }
    }
}
