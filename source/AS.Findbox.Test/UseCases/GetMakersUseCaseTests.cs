using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Application.Adapters.Makers;
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
    public class GetMakersUseCaseTests
    {
        private HttpClient _httpClient;
        private ILogger<CarScraperService> _logger;

        public GetMakersUseCaseTests()
        {
            _httpClient = _httpClient.BuildGetMakers();
            _logger = new Mock<ILogger<CarScraperService>>().Object;
        }

        [TestMethod]
        public async Task Execute_WhenIsOk_ReturnOk()
        {
            _httpClient = _httpClient.SetupMakers();
            var useCase = new GetMakersUseCase(new CarScraperService(_logger, _httpClient));
            var request = new GetMakersRequest();
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count,93);
        }

        [TestMethod]
        public async Task Execute_WhenInvalidResponse_ReturnNoOptions()
        {
            _httpClient = _httpClient.SetupInvalidMakers();
            var useCase = new GetMakersUseCase(new CarScraperService(_logger, _httpClient));
            var request = new GetMakersRequest();
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count, 0);
        }

       
    }
}
