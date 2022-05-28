using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Cars
{
    public class CarResponse
    {

        public Guid Id { get; }
        public string Make { get; }
        public string Model { get; }
        public string Title { get; }
        public string Condition { get; }
        public int Mileage { get; }
        public decimal Value { get; }
        public decimal Rating { get; }
        public string PictureLink { get; }
        public string Picture { get; protected set; }

        public CarResponse(Guid id, string make, string model, string title, string condition, int mileage, decimal value, decimal rating, string pictureLink)
        {
            this.Id = id;
            this.Make = make;
            this.Model = model;
            this.Title = title;
            this.Condition = condition.Contains("Certified") ? "Certified" : condition;
            this.Mileage = mileage;
            this.Value = value;
            this.Rating = rating;
            this.PictureLink = pictureLink;
        }

        public void AddPicture(string picture)
        {
            this.Picture = picture;
        }
    }
}
