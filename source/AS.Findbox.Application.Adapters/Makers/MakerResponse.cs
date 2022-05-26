using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Makers
{
    public class MakerResponse
    {
        public string Id { get; protected set; }
        public string Description { get; protected set; }

        public MakerResponse(string id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
