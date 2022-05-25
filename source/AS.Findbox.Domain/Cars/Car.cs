using AS.Findbox.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Domain.Cars
{
    internal class Car : ICar
    {
        internal Car()
        {
        }

        public string AdTitle { get; protected set; }
        public string Make { get; protected set; }
        public string Model { get; protected set; }
        public Condition Condition { get; protected set; }
        public int Mileage { get; protected set; }
        public double Value { get; protected set; }
        public int Rating { get; protected set; }
        public string Picture { get; protected set; }
    }
}
