using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AS.Findbox
{
    internal class Program
    {
        public static ServiceProvider Provider { get; private set; }

        static void Main(string[] args)
        {
            var servicesConfiguration = new ServicesCollectionConfiguration();
            Provider = servicesConfiguration.ServiceProvider;
            var carPresenter = Provider.GetService<CarScraperPresenter>();


            Console.WriteLine("Starting...");

            carPresenter.Login();
            var makers = carPresenter.GetMakers();
            var makeSelected = GetMake(makers);

            var models = carPresenter.GetModels(makeSelected.Id);
            var modelSelected = GetModel(models);
            var cars = carPresenter.GetSearchCars(makeSelected.Id, modelSelected.Slug);
            Console.WriteLine("End...");
            Console.ReadKey();
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
