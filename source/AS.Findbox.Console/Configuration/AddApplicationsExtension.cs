using AS.Findbox.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Configuration
{
    public static class AddApplicationsExtension
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            return services.AddScoped<LoginUseCase>()
                .AddScoped<GetMakersUseCase>()
                .AddScoped<GetModelsUseCase>()
                .AddScoped<GetSearchUseCase>()
                ;
            

        }
    }
}
