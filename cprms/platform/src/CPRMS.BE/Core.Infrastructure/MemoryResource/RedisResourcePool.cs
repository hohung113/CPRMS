using Core.Application.Interfaces;
using Core.Utility.Json;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;namespace Core.Infrastructure.MemoryResource
{
    public class RedisResourcePool : IRedisResourcePool
    {
        private readonly ILogger<RedisResourcePool> _logger;
        private readonly ConnectionMultiplexer redisConnection;
        private readonly IDatabase redisDb;

        public RedisResourcePool(IServiceProvider serviceProvider, string ridesConnection)
        {
            //_logger = serviceProvider.GetRequiredService<ILogger<RedisResourcePool>>();
            redisConnection = ConnectionMultiplexer.Connect(ridesConnection);
            redisDb = redisConnection.GetDatabase(1);

        }

        public ConnectionMultiplexer RetrieveRedisConnection()
        {
            return redisConnection;
        }

        ~RedisResourcePool()
        {
            redisConnection.Close();
        }

        public async Task<string> GetString(string key)
        {
            string? result = await redisDb.StringGetAsync(key);
            return result ?? String.Empty;
        }

        public async Task<TObject?> GetObjectAsync<TObject>(string key)
        {
            string? result = await redisDb.StringGetAsync(key);
            if (String.IsNullOrWhiteSpace(result))
            {
                return default;
            }
            TObject? obj = JsonSerializerHelper.Deserialize<TObject>(result);
            return obj;
        }

        public async Task<bool> SetSerializeObject<TObject>(string key, TObject value)
        {
            string seril = JsonSerializerHelper.Serialize(value);
            bool result = await redisDb.StringSetAsync(key, seril);
            return result;
        }

        public async Task<List<string>> GetListAsync(string key)
        {
            RedisValue[] storedList = await redisDb.ListRangeAsync(key);
            List<string> result = [.. storedList];
            return result;
        }

        public async Task<bool> SetValue(string key, string value)
        {
            return await redisDb.StringSetAsync(key, value);
        }

        public async Task<bool> SetObjectValueAsync<TObject>(string key, TObject value, TimeSpan expiry)
        {
            string seril = JsonSerializerHelper.Serialize(value);
            return await redisDb.StringSetAsync(key, seril, expiry);
        }

        public async Task SetItemIntoListLeft(string key, string item)
        {
            await redisDb.ListLeftPushAsync(key, item);
        }

        public async Task<string> PopLastItem(string key)
        {
            RedisValue value = await redisDb.ListRightPopAsync(key);
            return value.ToString();
        }

        public async Task RemoveItemFromList(string key, string item)
        {
            await redisDb.ListRemoveAsync(key, item);
        }

        public async Task KeyDeleteAsync(string key)
        {
            await redisDb.KeyDeleteAsync(key);
        }

        ConnectionMultiplexer IRedisResourcePool.RetrieveRedisConnection()
        {
            return redisConnection;
        }
    }
}
