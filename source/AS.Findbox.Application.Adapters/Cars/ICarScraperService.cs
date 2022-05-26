using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Cars
{
    public interface ICarScraperService
    {
        Task<List<MakerResponse>> GetMakers();
        Task<List<ModelResponse>> GetModels(string make);
        Task<List<CarResponse>> GetCars(string make, string model);
    }
}
