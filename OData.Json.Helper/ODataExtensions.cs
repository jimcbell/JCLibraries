using System.Net.Http.Json;
using System.Text.Json;

namespace OData.Json.Helper;

public static class ODataExtensions
{
    private static readonly string _value = "value";
    public static async Task<T?> ParseContent<T>(this HttpResponseMessage response)
    {
        JsonElement jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();
        jsonElement.TryGetProperty(_value, out JsonElement content);
        T? deserializedObject = JsonSerializer.Deserialize<T>(content);
        if (deserializedObject is null)
        {
            throw new Exception("Deserialized object is null");
        }
        return deserializedObject;
    }
}
