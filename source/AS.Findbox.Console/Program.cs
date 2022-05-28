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
            carPresenter.Login(user.Email, user.Password);

            var makeSelected = SelectMake(carPresenter);

            Console.Clear();
            var modelSelected = SelectModel(carPresenter, makeSelected);
            if (modelSelected != null)
            {
                Console.Clear();
                var cars = carPresenter.GetSearchCars(makeSelected.Id, modelSelected.Slug);
                Console.WriteLine("All cars have been saved in the database...");
            }
          
            Console.WriteLine("End...");
            Console.ReadKey();
        }

        private static ModelResponse SelectModel(CarScraperPresenter carPresenter, MakerResponse makeSelected)
        {
            var models = carPresenter.GetModels(makeSelected.Id);
            if (models == null || models.Count == 0)
            {
                Console.WriteLine("Models not found.");         
                return null;
            }

            var modelSelected = GetModel(models);
            return modelSelected;
        }

        private static MakerResponse SelectMake(CarScraperPresenter carPresenter)
        {
            var makers = carPresenter.GetMakers();
            var makeSelected = GetMake(makers);
            return makeSelected;
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
            if (!int.TryParse(make, out int id) || id > makers.Count + 1 || id < 1)
            {
                Console.WriteLine("Invalid option.");
                return GetMake(makers);
            }

            var makeSelected = makers[id - 1];
            return makeSelected;
        }

        private static ModelResponse GetModel(List<ModelResponse> models)
        {
            Console.WriteLine("Select model:");
            var model = Console.ReadLine();
            if (!int.TryParse(model, out int id) || id > models.Count + 1 || id < 1)
            {
                Console.WriteLine("Invalid option.");
                return GetModel(models);
            }
            
            return models[id - 1];
        }
    }
}
