using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AS.Findbox
{
    internal class Program
    {
        private static IConfiguration configuration;

        public static ServiceProvider Provider { get; private set; }

        static void Main(string[] args)
        {
            Configure();

            var carPresenter = Provider.GetService<CarScraperPresenter>();
            Console.WriteLine("Starting...");

            //var user = new UserConfiguration();
            var user = configuration.GetSection("User").Get<UserConfiguration>();
            carPresenter.Login(user.Email,user.Password);

           
            var makers = carPresenter.GetMakers();
            var makeSelected = GetMake(makers);

             Console.Clear();
            var models = carPresenter.GetModels(makeSelected.Id);
            var modelSelected = GetModel(models);
            Console.Clear();
            var cars = carPresenter.GetSearchCars(makeSelected.Id, modelSelected.Slug);
            Console.WriteLine("All cars have been saved in the database...");
            Console.WriteLine("End...");
            Console.ReadKey();
        }

        private static void Configure()
        {
            var servicesConfiguration = new ServicesCollectionConfiguration();
            Provider = servicesConfiguration.ServiceProvider;

            var builder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: false);

            configuration = builder.Build();
        }

        private static MakerResponse GetMake(List<MakerResponse> makers)
        {
            Console.WriteLine("Select make:");
            var make = Console.ReadLine();
            if (int.TryParse(make, out int id))
            {
                if (id > makers.Count + 1 || id < 1)
                {
                    Console.WriteLine("Invalid option.");
                    GetMake(makers);
                }
            }
            var makeSelected = makers[id - 1];
            return makeSelected;
        }

        private static ModelResponse GetModel(List<ModelResponse> models)
        {
            Console.WriteLine("Select model:");
            var model = Console.ReadLine();
            if (int.TryParse(model, out int id))
            {
                if (id > models.Count + 1 || id < 1)
                {
                    Console.WriteLine("Invalid option.");
                    GetModel(models);
                }
            }
            return models[id - 1];
        }
    }
}
