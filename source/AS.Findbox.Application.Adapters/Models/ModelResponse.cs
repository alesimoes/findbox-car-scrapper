using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Models
{
    public class ModelResponse
    {
        public string Name { get; protected set; }
        public string Make { get; protected set; }
        public string Slug { get; protected set; }

        public ModelResponse(string make, string name, string slug)
        {
            this.Make = make;
            this.Name = name;
            this.Slug = slug;
        }
    }
}
