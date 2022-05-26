using Microsoft.Extensions.DependencyInjection;
using AS.Findbox.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Configuration
{
    internal class ServicesCollectionConfiguration
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public ServicesCollectionConfiguration()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices()
                .AddRepositories()
                .AddApplications()
                .AddPresenters()
                .AddMediators()
                ;
            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProvider.CreateScope();
        }
    }
}
