using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using FluentMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox
{
    public class CarScraperPresenter
    {
        private IMediator _mediator;

        public CarScraperPresenter(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public void Login(string email, string password)
        {
            var response = this._mediator.SendAsync<LoginResponse>(new LoginRequest(email, password));
            response.Wait();

            Console.WriteLine(response.Result.User);
        }

        public List<MakerResponse> GetMakers()
        {
            var response = this._mediator.SendAsync<List<MakerResponse>>(new GetMakersRequest());
            response.Wait();
            Console.WriteLine("Makers:");
            int option = 1;
            foreach (var make in response.Result)
            {
                Console.WriteLine($"{option} - {make.Description}");
                option++;
            }
            return response.Result;
        }

        public List<ModelResponse> GetModels(string make)
        {
            var response = this._mediator.SendAsync<List<ModelResponse>>(new GetModelsRequest(make));
            response.Wait();

            Console.WriteLine("Models:");
            int option = 1;
            foreach (var model in response.Result)
            {
                Console.WriteLine($"{option} - {model.Name}");
                option++;
            }
            return response.Result;
        }

        public List<CarResponse> GetSearchCars(string make, string model)
        {
            Console.WriteLine("Waiting...");
            var response = this._mediator.SendAsync<List<CarResponse>>(new GetSearchRequest(make, model));
            response.Wait();

            Console.WriteLine("Cars found:");
            int option = 1;
            foreach (var car in response.Result)
            {
                Console.WriteLine($"{option} - {car.Title} - Mileage:{car.Mileage} ${car.Value} - Rating:{car.Rating}");
                option++;
            }
            return response.Result;
        }

    }
}
