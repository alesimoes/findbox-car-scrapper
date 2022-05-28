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
    public static class LoginUseCaseHttpClientBuilder
    {
        private static MockHttpMessageHandler _mockHttp;

        public static HttpClient BuildLogin(this HttpClient httpClient)
        {
            _mockHttp = new MockHttpMessageHandler();
            httpClient = new HttpClient(_mockHttp);
            httpClient.BaseAddress = new Uri("http://test/");
            return httpClient;
        }

        public static HttpClient SetupLogin(this HttpClient httpClient)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            _mockHttp.When($"{httpClient.BaseAddress}signin/").Respond(req =>
            {
                if (req.Method == HttpMethod.Post)
                {
                    response.Content = new StringContent(Responses.CarHome);
                }
                else
                {
                    response.Content = new StringContent(Responses.SigninPage);
                }
                return response;
            }
           );

            return httpClient;
        }

        public static HttpClient SetupUnauthorizedLogin(this HttpClient httpClient)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            _mockHttp.When($"{httpClient.BaseAddress}signin/").Respond(req =>
            {
                if (req.Method == HttpMethod.Post)
                {
                    response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    response.Content = new StringContent(Responses.CarHome);
                }
                else
                {
                    response.Content = new StringContent(Responses.SigninPage);
                }
                return response;
            });

            return httpClient;
        }

        public static HttpClient SetupInvalidResponseLogin(this HttpClient httpClient)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            _mockHttp.When($"{httpClient.BaseAddress}signin/").Respond(req =>
            {
                if (req.Method == HttpMethod.Post)
                {
                    response.Content = new StringContent(Responses.SigninPage);
                }
                else
                {
                    response.Content = new StringContent(Responses.SigninPage);
                }
                return response;
            });

            return httpClient;
        }
    }
}
