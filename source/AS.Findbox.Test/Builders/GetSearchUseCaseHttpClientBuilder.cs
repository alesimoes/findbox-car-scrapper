using AS.Findbox.Application.Adapters.Login;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Test.Builders
{
    public static class GetSearchUseCaseHttpClientBuilder
    {
        private static MockHttpMessageHandler _mockHttp;

        public static HttpClient BuildGetSearch(this HttpClient httpClient)
        {
            _mockHttp = new MockHttpMessageHandler();
            httpClient = new HttpClient(_mockHttp);
            httpClient.BaseAddress = new Uri("http://test/");
            return httpClient;
        }

        public static HttpClient SetupCars(this HttpClient httpClient, string make, string model)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            _mockHttp.When($"{httpClient.BaseAddress}shopping/results/?stock_type=all&makes%5B%5D={make}&models%5B%5D={model}&list_price_max=&maximum_distance=20&zip=").Respond(req =>
            {
                response.Content = new StringContent(Responses.SearchResult);
                return response;
            }
           );

            _mockHttp.When("https://platform.cstatic-images.com/*").Respond(req =>
            {
                response.Content = new StringContent("image");
                return response;
            });
            return httpClient;
        }

        public static HttpClient SetupInvalidCars(this HttpClient httpClient)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            _mockHttp.When($"{httpClient.BaseAddress}").Respond(req =>
            {
                response.Content = new StringContent(Responses.SigninPage);

                return response;
            });

            return httpClient;
        }
    }
}
