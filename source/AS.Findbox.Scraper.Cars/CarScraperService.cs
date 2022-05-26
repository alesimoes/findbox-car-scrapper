using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Scraper.Cars.Model;
using AS.Findbox.Scraper.Cars.Utils;
using System;
using System.Collections.Generic;
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

        public CarScraperService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<List<CarResponse>> GetCars(string make, string model)
        {
            var responseGet = await this._client.GetAsync($"/shopping/results/?stock_type=all&makes%5B%5D={make}&models%5B%5D={model}&list_price_max=&maximum_distance=20&zip=");
            var carList = await responseGet.Content.GetExpression("(<div.*class=\"vehicle-card-main.*>+\\s*)|(<div.*class=\"image-wrap\" data-index=\"0\".+\\s*)(<img .* class=\"vehicle-image.*>)*|(<p.*class=\"stock-type.*>)|(<h2.*class=\"title.*>.+)|(<div.*class=\"mileage.*>.+)|(<span.*class=\"primary-price.*>)|(<span.*class=\"sds-rating__count.*>)");
            var list = new List<CarResponse>();
            for (int i = 0; i < carList.Count; i+=7)
            {
                var header = carList[i];
                if (!header.Contains("vehicle-card-main")){
                    i -= 6;
                    continue;
                }

                var image = HtmlExtractor.GetImageLink(carList[i+1]);
                var condition = HtmlExtractor.GetTagText(carList[i+2]);
                var title = HtmlExtractor.GetTagText(carList[i+3]);
                var mileage = HtmlExtractor.GetTagText(carList[i+4]);
                var price = HtmlExtractor.GetTagText(carList[i+5]);
                var rating = HtmlExtractor.GetTagText(carList[i+6]);
                var car = new CarResponse(make, model, title, condition, mileage,price,rating, image);
                list.Add(car);
            }

            return list;
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
