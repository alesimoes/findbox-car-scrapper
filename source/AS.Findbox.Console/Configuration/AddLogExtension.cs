using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Domain.Cars;
using AS.Findbox.Repository.MongoDB;
using AS.Findbox.Scraper.Cars;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Configuration
{
    public static class AddLogExtension
    {
        public static IServiceCollection AddLog(this IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole());
            return services;
        }
    }
}
