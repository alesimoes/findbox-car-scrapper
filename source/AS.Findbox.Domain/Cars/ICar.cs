using AS.Findbox.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Domain.Cars
{
    public interface ICar
    {
        public string AdTitle { get; }
        public string Make { get;  }
        public string Model { get; }
        public Condition Condition { get; }
        public int Mileage { get; }
    }
}
