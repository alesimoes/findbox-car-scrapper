
using AS.Findbox.Domain.Cars;
using AS.Findbox.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;


namespace AS.Findbox.Test.Domain
{
    [TestClass]
    public class CarTests
    {
     
        public CarTests()
        {
          
        }

        [TestMethod]
        public async Task Execute_WhenIsOk_ReturnOk()
        {
            var car = new Car(Guid.NewGuid(), "title", "make", "model", "Used", 123, (decimal)12.3, (decimal)12.3, "picture");
            Assert.IsNotNull(car);
        }

        [TestMethod]
        public async Task Execute_WhenCarCreated_ReturnOk()
        {
            var car = new Car(Guid.NewGuid(), "title", "make", "model", "Used", 123, (decimal)12.3, (decimal)12.3, "picture");
            Assert.IsNotNull(car);
            Assert.IsNotNull(car.Id);
            Assert.IsNotNull(car.AdTitle);
            Assert.IsNotNull(car.Make);
            Assert.IsNotNull(car.Model);
            Assert.IsNotNull(car.Condition);
            Assert.IsNotNull(car.Value);
            Assert.IsNotNull(car.Mileage);
            Assert.IsNotNull(car.Rating);
            Assert.IsNotNull(car.Picture);
        }

        [TestMethod]
        [ExpectedException(typeof(FieldValidationException))]
        public async Task Execute_WhenTitleIsEmpty_ReturnDomainException()
        {
            var car = new Car(Guid.NewGuid(), "", "make", "model", "Used", 123, (decimal)12.3, (decimal)12.3, "picture");
        }

        [TestMethod]
        [ExpectedException(typeof(FieldValidationException))]
        public async Task Execute_WhenMakeIsEmpty_ReturnDomainException()
        {
            var car = new Car(Guid.NewGuid(), "title", "", "model", "Used", 123, (decimal)12.3, (decimal)12.3, "picture");
        }

        [TestMethod]
        [ExpectedException(typeof(FieldValidationException))]
        public async Task Execute_WhenModelIsEmpty_ReturnDomainException()
        {
            var car = new Car(Guid.NewGuid(), "title", "make", "", "Used", 123, (decimal)12.3, (decimal)12.3, "picture");
        }

        [TestMethod]
        [ExpectedException(typeof(FieldValidationException))]
        public async Task Execute_WhenEmptyCondition_ReturnDomainException()
        {
            var car = new Car(Guid.NewGuid(), "title", "make", "model", "", 123, (decimal)12.3, (decimal)12.3, "picture");
        }

        [TestMethod]
        [ExpectedException(typeof(FieldValidationException))]
        public async Task Execute_WhenInvalidCondition_ReturnDomainException()
        {
            var car = new Car(Guid.NewGuid(), "title", "make", "model", "INVALID", 123, (decimal)12.3, (decimal)12.3, "picture");
        }

        [TestMethod]
        [ExpectedException(typeof(FieldValidationException))]
        public async Task Execute_WhenPictureIsEmpty_ReturnDomainException()
        {
            var car = new Car(Guid.NewGuid(), "title", "make", "model", "New", 123, (decimal)12.3, (decimal)12.3, "");
        }

        
    }
}
