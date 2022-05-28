using AS.Findbox.Domain.Enums;
using AS.Findbox.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Domain.Cars
{
    public class Car
    {
        public Guid Id { get; protected set; }
        public string AdTitle { get; protected set; }
        public string Make { get; protected set; }
        public string Model { get; protected set; }
        public Condition Condition { get; protected set; }
        public int Mileage { get; protected set; }
        public decimal Value { get; protected set; }
        public decimal Rating { get; protected set; }
        public string Picture { get; protected set; }

        public Car(Guid id, string title, string make, string model, string condition, int mileage, decimal value, decimal rating, string picture)
        {
            this.Id = id;
            this.AdTitle = title;
            this.Make = make;
            this.Model = model;
            this.Condition = GetCondition(condition);
            this.Mileage = mileage;
            this.Value = value;
            this.Rating = rating;
            this.Picture = picture;

            this.Validate();
        }

        private static Condition GetCondition(string condition)
        {
            Condition result;
            if(Enum.TryParse(condition, out result))
            {
                return result;
            }
            else
            {
                throw new FieldValidationException(Fields.Condition);
            }           
        }

        private void Validate()
        {
            
            if (string.IsNullOrEmpty(AdTitle))
            {
                throw new FieldValidationException(Fields.Title);
            }

            if(string.IsNullOrEmpty(Make))
            {
                throw new FieldValidationException(Fields.Make);
            }

            if (string.IsNullOrEmpty(Model))
            {
                throw new FieldValidationException(Fields.Model);
            }

            if (string.IsNullOrEmpty(Picture))
            {
                throw new FieldValidationException(Fields.Model);
            }
        }
    }
}
