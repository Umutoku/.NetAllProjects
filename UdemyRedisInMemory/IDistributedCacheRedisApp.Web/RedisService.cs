namespace IDistributedCacheRedisApp.Web
{
    using StackExchange.Redis;
    public class RedisService
    {
        ConnectionMultiplexer connectionMultiplexer;
        public void Connect() => connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:1453");
        public IDatabase GetDb(int db) => connectionMultiplexer.GetDatabase(db);
    }
}
