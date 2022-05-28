using As.Findbox.Repository.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Repository.MongoDB
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddMongoDb(
          this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
         
            var builder = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: false);

            IConfiguration configuration = builder.Build();

            var serverConfig = new ServerConfig();
            configuration.Bind(serverConfig);

            services.AddScoped<MongoDbContext>(s => new MongoDbContext(serverConfig.MongoDB));
            return services;
        }

    }
}
