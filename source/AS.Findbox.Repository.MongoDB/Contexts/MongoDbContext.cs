using AS.Findbox.Domain.Cars;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace As.Findbox.Repository.MongoDB
{
    public class MongoDbContext : IDisposable
    {
        private readonly IMongoDatabase _db;
        private readonly IClientSessionHandle _session;
        private bool _disposed;

        public MongoDbContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
            this._session = client.StartSession();
        }
        public IMongoCollection<ICar> Posts => _db.GetCollection<ICar>("Car");
     
        internal IClientSessionHandle Session => _session;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    this.Session.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
