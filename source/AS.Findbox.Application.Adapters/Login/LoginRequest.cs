using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Login
{
    public class LoginRequest
    {

        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public LoginRequest(string email, string password)
        {
            this.Email = email;
            this.Password = password;
            this.Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentNullException("Email");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentNullException("Password");
            }
        }
    }
}
