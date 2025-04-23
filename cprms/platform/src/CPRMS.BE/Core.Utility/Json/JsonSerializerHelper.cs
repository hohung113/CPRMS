namespace Core.Utility.Json
{
    public class JsonSerializerHelper
    {
        readonly static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static TValue? Deserialize<TValue>(string json)
        {
            return JsonConvert.DeserializeObject<TValue>(json, _settings);
        }

        public static TValue? DeserializeSafe<TValue>(string json)
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                return default;
            }
            try
            {
                return JsonConvert.DeserializeObject<TValue>(json, _settings);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                return default;
            }
        }

        public static string Serialize<TValue>(TValue value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }

        public static string Serialize<TValue>(TValue value, JsonSerializerSettings serilaizerSetting)
        {
            return JsonConvert.SerializeObject(value, serilaizerSetting);
        }

        public static string? SerializeAllowNull<TValue>(TValue value)
        {
            if (value == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(value, _settings);
        }

        public static object? Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

    }
}
