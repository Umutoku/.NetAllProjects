using StackExchange.Redis;

namespace Order.API.RedisServices
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(IConfiguration configuration)
        {
            var host = configuration.GetValue<string>("Redis:Host");    
            var port = configuration.GetValue<string>("Redis:Port");
            _redis = ConnectionMultiplexer.Connect($"{host}:{port}");

        }

        public ConnectionMultiplexer GetConnectionMultiplexer => _redis; // redis bağlantı nesnesi

        public IDatabase GetDb(int db = 1)
        {
            return _redis.GetDatabase(db);
        }
    }
}
