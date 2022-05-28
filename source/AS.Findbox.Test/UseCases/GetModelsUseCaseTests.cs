using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Application.UseCases;
using AS.Findbox.Scraper.Cars;
using AS.Findbox.Test.Builders;
using Microsoft.Extensions.Logging;
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
    public class GetModelsUseCaseTests
    {
        private HttpClient _httpClient;
        private ILogger<CarScraperService> _logger;

        public GetModelsUseCaseTests()
        {
            _httpClient = _httpClient.BuildGetModels();
            _logger = new Mock<ILogger<CarScraperService>>().Object;
        }

        [TestMethod]
        public async Task Execute_WhenIsOk_ReturnOk()
        {
            _httpClient = _httpClient.SetupModels();
            var useCase = new GetModelsUseCase(new CarScraperService(_logger, _httpClient));
            var request = new GetModelsRequest("BMW");
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count, 106);
        }

        [TestMethod]
        public async Task Execute_WhenInvalidResponse_ReturnNoOptions()
        {
            _httpClient = _httpClient.SetupInvalidModels();
            var useCase = new GetMakersUseCase(new CarScraperService(_logger, _httpClient));
            var request = new GetMakersRequest();
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count, 0);
        }


    }
}
