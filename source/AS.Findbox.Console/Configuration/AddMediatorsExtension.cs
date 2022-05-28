using AS.Findbox.Application.Adapters.Cars;
using AS.Findbox.Application.Adapters.Login;
using AS.Findbox.Application.Adapters.Makers;
using AS.Findbox.Application.Adapters.Models;
using AS.Findbox.Application.UseCases;
using FluentMediator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Configuration
{
    public static class AddMediatorsExtension
    {
        public static IServiceCollection AddMediators(this IServiceCollection services)
        {
            services.AddFluentMediator(
                builder =>
                {
                    builder.On<LoginRequest>().PipelineAsync()
                    .Return<LoginResponse, LoginUseCase>((handler, request) => handler.Execute(request));
                    builder.On<GetMakersRequest>().PipelineAsync()
                    .Return<List<MakerResponse>, GetMakersUseCase>((handler, request) => handler.Execute(request));
                    builder.On<GetModelsRequest>().PipelineAsync()
                    .Return<List<ModelResponse>, GetModelsUseCase>((handler, request) => handler.Execute(request));
                    builder.On<GetSearchRequest>().PipelineAsync()
                    .Return<List<CarResponse>, GetSearchUseCase>((handler, request) => handler.Execute(request));

                });

            return services;
        }
    }
}
