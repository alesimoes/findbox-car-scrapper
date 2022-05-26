using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Domain.Cars;
using AS.Findbox.Scraper.Cars;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Configuration
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICarScraperService, CarScraperService>()
              .AddScoped<ILoginService, LoginScraperService>()
              .AddScoped<ICarService, CarService>()
              .AddScoped<HttpClient>(provider =>
              {
                  var cookieContainer = new CookieContainer();
                  var handler = new HttpClientHandler()
                  {
                      CookieContainer = cookieContainer
                  };
                  var baseAdderss = new Uri("https://www.cars.com/");
                  cookieContainer.Add(baseAdderss, new Cookie("Cookie", "scraper"));
                  var client = new HttpClient(handler);
                  client.BaseAddress = baseAdderss;
                  client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("PostmanRuntime", "7.29.0"));

                  return client;
              });
        }
    }
}
