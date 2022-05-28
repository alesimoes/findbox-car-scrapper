using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Application.UseCases;
using AS.Findbox.Domain.Cars;
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
    public class GetSearchUseCaseTests
    {
        private HttpClient _httpClient;
        private ILogger<CarScraperService> _logger;
        private Mock<ICarRepository> _carRepository;
        private CarService _carService;

        public GetSearchUseCaseTests()
        {
            _httpClient = _httpClient.BuildGetSearch();
            _logger = new Mock<ILogger<CarScraperService>>().Object;
            _carRepository = new Mock<ICarRepository>();
            _carRepository.Build();
            _carService = new CarService(_carRepository.Object);
        }

        [TestMethod]
        public async Task Execute_WhenIsOk_ReturnOk()
        {
            _httpClient = _httpClient.SetupCars("BMW", "X1");
            var useCase = new GetSearchUseCase(_carService, new CarScraperService(_logger, _httpClient));
            var request = new GetSearchRequest("BMW","X1");
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count, 10);
        }

        [TestMethod]
        public async Task Execute_WhenInvalidResponse_ReturnNoOptions()
        {
            _httpClient = _httpClient.SetupInvalidCars();
            var useCase = new GetSearchUseCase(_carService, new CarScraperService(_logger, _httpClient));
            var request = new GetSearchRequest("BMW", "X1");
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Execute_WhenInvalidRequestModel_ReturnException()
        {
            _httpClient = _httpClient.SetupCars("BMW", "X1");
            var useCase = new GetSearchUseCase(_carService, new CarScraperService(_logger, _httpClient));
            var request = new GetSearchRequest("BMW","");
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count, 10);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Execute_WhenInvalidRequestMake_ReturnException()
        {
            _httpClient = _httpClient.SetupCars("BMW", "X1");
            var useCase = new GetSearchUseCase(_carService, new CarScraperService(_logger, _httpClient));
            var request = new GetSearchRequest("", "X1");
            var response = await useCase.Execute(request);
            Assert.AreEqual(response.Count, 10);
        }
    }
}
