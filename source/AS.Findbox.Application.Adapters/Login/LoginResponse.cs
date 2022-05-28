using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Login
{
    public class LoginResponse
    {

        public string User { get; }

        public LoginResponse(string user)
        {
            this.User = user;

            this.Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(User))
            {
                throw new ArgumentNullException("User");
            }
        }
    }
}
