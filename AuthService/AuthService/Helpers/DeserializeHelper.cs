using System.Text.Json;

namespace AuthService.Helpers
{
    public static class DeserializeHelper
    {
        public static T DeserializeMethod<T> (string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
