using first_project.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace first_project.Infrastructure
{
    public class MongoClientFactory
    {
        private readonly IMongoClient _client;

        public MongoClientFactory(IOptions<MongoDbSettings> options)
        {
            if (string.IsNullOrEmpty(options.Value.ConnectionString))
            {
                throw new ArgumentNullException("ConnectionString", "ConnectionString is required");
            }

            _client = new MongoClient(options.Value.ConnectionString);
        }

        public IMongoClient GetClient() => _client;
    }
}
