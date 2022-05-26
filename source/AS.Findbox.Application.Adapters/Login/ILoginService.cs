using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.Adapters.Login
{
    public interface ILoginService
    {        Task<string> Login(string email, string password);
    }
}
