using AS.Findbox.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Configuration
{
    public static class AddPresentersExtension
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            return services.AddScoped<CarScraperPresenter>();

        }
    }
}
