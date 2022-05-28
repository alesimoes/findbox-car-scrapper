using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Scraper.Cars.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException() : base(Messages.LoginFailedException)
        {
        }
    }
}
