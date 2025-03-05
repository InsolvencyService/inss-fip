using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace INSS.FIP.Web.Extensions;

[ExcludeFromCodeCoverage]
public static class SessionExtensions
{
    public static void SetObject<T>(this ISession session, string key, T? value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T? GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}
