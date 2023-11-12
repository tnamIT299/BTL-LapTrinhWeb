using System.Text.Json;
namespace Client_Home.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key,object value) { 
        session.SetString(key,JsonSerializer.Serialize(value));
        }
        public static T? GetJson<T>( this ISession session, string key)
        {
            var sesstionData = session.GetString(key);
            return sesstionData == null ? default(T) : JsonSerializer.Deserialize<T>(sesstionData);
        }
    }
}
