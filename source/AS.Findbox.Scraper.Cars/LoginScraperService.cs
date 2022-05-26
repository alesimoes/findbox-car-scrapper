using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Scraper.Cars.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AS.Findbox.Scraper.Cars
{
    public class LoginScraperService : ILoginService
    {
        private readonly HttpClient _client;

        public LoginScraperService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<string> Login(string email, string password)
        {
            var responseGet = await this._client.GetAsync("signin/");
            var token = await responseGet.Content.GetInputValue("_csrf_token");

            var formContent = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("_csrf_token", token),
                    new KeyValuePair<string, string>("user[email]", email),
                    new KeyValuePair<string, string>("user[password]", password),
                    new KeyValuePair<string, string>("user[redirect_path]", "/")
            });

            var response = await this._client.PostAsync("signin/", formContent);
            var user = await response.Content.GetSpanText("desktop-nav-user-name");

            return user;
        }
    }
}
