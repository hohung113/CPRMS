namespace Core.Application.Interfaces
{
    public interface IRedisResourcePool
    {
        public ConnectionMultiplexer RetrieveRedisConnection();
        public Task<string> GetString(string key);
        public Task<List<string>> GetListAsync(string key);
        public Task SetItemIntoListLeft(string key, string item);
        public Task RemoveItemFromList(string key, string item);
        public Task KeyDeleteAsync(string key);
        public Task<bool> SetValue(string key, string value);
        public Task<bool> SetObjectValueAsync<TObject>(string key, TObject value, TimeSpan expiry);
        public Task<TObject?> GetObjectAsync<TObject>(string key);
        public Task<bool> SetSerializeObject<TObject>(string key, TObject value);
    }
}
