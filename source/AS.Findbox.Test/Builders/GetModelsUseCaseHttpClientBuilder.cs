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
    public static class GetModelsUseCaseHttpClientBuilder
    {
        private static MockHttpMessageHandler _mockHttp;

        public static HttpClient BuildGetModels(this HttpClient httpClient)
        {
            _mockHttp = new MockHttpMessageHandler();
            httpClient = new HttpClient(_mockHttp);
            httpClient.BaseAddress = new Uri("http://test/");
            return httpClient;
        }

        public static HttpClient SetupModels(this HttpClient httpClient)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            _mockHttp.When($"{httpClient.BaseAddress}").Respond(req =>
            {
                response.Content = new StringContent(Responses.CarHome);
                return response;
            }
           );

            return httpClient;
        }

        public static HttpClient SetupInvalidModels(this HttpClient httpClient)
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
