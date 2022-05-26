using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Cars
{
    public class CarResponse
    {
        
        public CarResponse(string make, string model, string title, string condition, string mileage, string value, string rating, string picture)
        {
            this.Make = make;
            this.Model = model;
            this.Title = title;
            this.Condition = condition;
            this.Mileage = mileage;
            this.Value = value;
            this.Rating = rating;
            this.Picture = picture;
        }

        public string Make { get; }
        public string Model { get; }
        public string Title { get; }
        public string Condition { get; }
        public string Mileage { get; }
        public string Value { get; }
        public string Rating { get; }
        public string Picture { get; }
    }
}
