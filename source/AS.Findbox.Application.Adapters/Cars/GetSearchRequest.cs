using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Cars
{
    public class GetSearchRequest
    {
        public string Make { get; protected set; }
        public string Model { get; protected set; }

        public GetSearchRequest(string make, string model)
        {
            Make = make;
            Model = model;

            this.Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Make))
            {
                throw new ArgumentNullException("Make");
            }

            if (string.IsNullOrEmpty(Model))
            {
                throw new ArgumentNullException("Model");
            }
        }
    }
}
