using AS.Findbox.Application.Adapters.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Application.UseCases
{
    public class LoginUseCase
    {
        private ILoginService _service;

        public LoginUseCase(ILoginService service)
        {
            _service = service;
        }

        public async Task<LoginResponse> Execute(LoginRequest request)
        {
            var user = await _service.Login(request.Email, request.Password);
            return new LoginResponse(user);
        }
    }
}
