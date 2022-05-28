using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Scraper.Cars.Exceptions;
using AS.Findbox.Scraper.Cars.Model;
using AS.Findbox.Scraper.Cars.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AS.Findbox.Scraper.Cars
{
    public class CarScraperService : ICarScraperService
    {
        private readonly HttpClient _client;
        private readonly ILogger<CarScraperService> _logger;

        public CarScraperService(ILogger<CarScraperService> logger, HttpClient client)
        {
            this._client = client;
            this._logger = logger;
        }

        public async Task<List<CarResponse>> GetCars(string make, string model)
        {
            var responseGet = await this._client.GetAsync($"/shopping/results/?stock_type=all&makes%5B%5D={make}&models%5B%5D={model}&list_price_max=&maximum_distance=20&zip=");
            var carList = await responseGet.Content.GetExpression("(<div class=\"vehicle-card *\".?\\s.+)|(<div.*class=\"image-wrap\" data-index=\"0\".+\\s*)(<img .* class=\"vehicle-image.*>)*|(<p.*class=\"stock-type.*>)|(<h2.*class=\"title.*>.+)|(<div.*class=\"mileage.*>.+)|(<span.*class=\"primary-price.*>)|(<span.*class=\"sds-rating__count.*>)");
            var cars = new List<CarResponse>();
            
            var regexMileage = new Regex("[0-9,]+");

            for (int i = 0; i < carList.Count; i+=7)
            {
                try
                {
                    var header = carList[i];
                    if (!header.Contains("vehicle-card"))
                    {
                        i -= 6;
                        continue;
                    }
                    var id = HtmlExtractor.GetGuid(carList[i]);
                    var image = HtmlExtractor.GetImageLink(carList[i + 1]);
                    var condition = HtmlExtractor.GetTagText(carList[i + 2]);
                    var title = HtmlExtractor.GetTagText(carList[i + 3]);
                    var mileage = regexMileage.Match(HtmlExtractor.GetTagText(carList[i + 4])).Value;
                    var price = HtmlExtractor.GetTagText(carList[i + 5]);
                    var rating = HtmlExtractor.GetTagText(carList[i + 6]);

                    var car = new CarResponse(Guid.Parse(id),
                        make, 
                        model, 
                        title,
                        condition, 
                        int.Parse(mileage, NumberStyles.Number, CultureInfo.CurrentUICulture),
                        Decimal.Parse(price),
                        Decimal.Parse(rating), image);
                    cars.Add(car);
                }
                catch
                {
                    // Just log the erros without stop the process.
                    _logger.LogError(Messages.CarImportFailed);
                }
            }

            cars = cars.Where(c=>c.Condition == "Used" || c.Condition == "New").Take(10).ToList();

            foreach (var car in cars)
            {
                try
                {
                    var imageResponse = Convert.ToBase64String(await (await this._client.GetAsync(car.PictureLink)).Content.ReadAsByteArrayAsync());
                    if (string.IsNullOrEmpty(imageResponse))
                    {
                         throw new CarScrapFailedException();
                    }

                    car.AddPicture(imageResponse);
                }
                catch
                {
                    // Just log the erros without stop the process.
                    _logger.LogError(Messages.CarImportGetPictureFailed,car.Id);
                }
            }

            return cars;
        }

        public async Task<List<MakerResponse>> GetMakers()
        {
            var responseGet = await this._client.GetAsync("/");
            var options = await responseGet.Content.GetSelectOptions("make");
            return options.Select(s => new MakerResponse(s.Item1, s.Item2)).ToList();
        }

        public async Task<List<ModelResponse>> GetModels(string make)
        {
            var responseGet = await this._client.GetAsync("/");
            var carOptions = await responseGet.Content.Parse<CarOptions>("CarsWeb.PageController.index");
            var selectedModels = carOptions.Models.Where(c => c.make_name.Equals(make, StringComparison.InvariantCultureIgnoreCase)).Select(s => new ModelResponse(s.make_name, s.name, s.slug));
            return selectedModels.ToList();
        }
    }
}
