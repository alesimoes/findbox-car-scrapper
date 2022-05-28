using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Domain.Cars;
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
    public static class CarRepositoryBuilder
    {
        public static Mock<ICarRepository> Build(this Mock<ICarRepository> mock)
        {
            mock = new Mock<ICarRepository>();
            mock.Setup(f => f.Find(Guid.Parse("0ee8b264-3496-4f6f-9251-38c4bb6ad748"))).ReturnsAsync(new Car(Guid.NewGuid(), "title", "make", "model", "Used", 123, (decimal)12.3, (decimal)12.3, "picture"));
            mock.Setup(f => f.Find(It.IsAny<Guid>())).ReturnsAsync(default(Car));
            return mock;
        }
    }
}
