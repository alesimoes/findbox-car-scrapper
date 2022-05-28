using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Application.UseCases;
using AS.Findbox.Scraper.Cars;
using AS.Findbox.Test.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace AS.Findbox.Test.UseCases
{
    [TestClass]
    public class LoginUseCaseTests
    {
        private HttpClient _httpClient;

        public LoginUseCaseTests()
        {
            _httpClient = _httpClient.BuildLogin();
        }

        [TestMethod]
        public async Task Execute_WhenIsOk_ReturnOk()
        {
            _httpClient = _httpClient.SetupLogin();
            var useCase = new LoginUseCase(new LoginScraperService(_httpClient));
            var request = new LoginRequest("email", "password");
            var user = await useCase.Execute(request);
            Assert.AreEqual(user.User, "Hi,   Alexandre Simoes");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Execute_WhenInvalidEmail_ReturnException()
        {
            _httpClient = _httpClient.SetupLogin();
            var useCase = new LoginUseCase(new LoginScraperService(_httpClient));
            var request = new LoginRequest(String.Empty, String.Empty);
            await useCase.Execute(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Execute_WhenInvalidPassword_ReturnException()
        {
            _httpClient = _httpClient.SetupLogin();
            var useCase = new LoginUseCase(new LoginScraperService(_httpClient));
            var request = new LoginRequest("email", String.Empty);
            await useCase.Execute(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Execute_WhenInvalidLogin_ReturnException()
        {
            _httpClient = _httpClient.SetupUnauthorizedLogin();
            var useCase = new LoginUseCase(new LoginScraperService(_httpClient));
            var request = new LoginRequest("email", String.Empty);
            await useCase.Execute(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Execute_WhenInvalidLogin_ReturnInvalidUser()
        {
            _httpClient = _httpClient.SetupInvalidResponseLogin();
            var useCase = new LoginUseCase(new LoginScraperService(_httpClient));
            var request = new LoginRequest("email", "password");
            var user = await useCase.Execute(request);           
        }
    }
}
