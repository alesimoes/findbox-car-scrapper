using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Domain.Cars;
using AS.Findbox.Repository.MongoDB;
using AS.Findbox.Scraper.Cars;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Configuration
{
    public static class AddRepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<ICarRepository, CarRespository>();
             
        }
    }
}
