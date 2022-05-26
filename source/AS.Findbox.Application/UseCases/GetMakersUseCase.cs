using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Makers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.UseCases
{
    public class GetMakersUseCase
    {
        private ICarScraperService _service;

        public GetMakersUseCase(ICarScraperService service)
        {
            _service = service;
        }

        public async Task<List<MakerResponse>> Execute(GetMakersRequest request)
        {
            var makers = await _service.GetMakers();
            return makers;
        }
    }
}
