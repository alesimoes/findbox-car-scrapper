using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Domain.Cars
{
    public interface ICarService
    {
        Task Save(Car car);
    }
}
