using Microsoft.Extensions.DependencyInjection;
using AS.Findbox.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AS.Findbox.Repository.MongoDB;
using System.Globalization;
using System.Threading;

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
                .AddMongoDb()
                .AddLog()              
                ;

            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProvider.CreateScope();

            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat =  new CultureInfo("en-US", false).NumberFormat;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
