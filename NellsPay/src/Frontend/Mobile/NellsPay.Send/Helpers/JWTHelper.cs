using System;
using System.Text;
namespace NellsPay.Send.Helpers
{
	public static class JwtHelper
{
    public static T? DecodePayload<T>(string jwtToken)
    {
        var parts = jwtToken.Split('.');
        if (parts.Length != 3)
            return default;

        string payload = PadBase64(parts[1]);
        var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
        return JsonSerializer.Deserialize<T>(json);
    }

    private static string PadBase64(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: return base64 + "==";
            case 3: return base64 + "=";
            case 0: return base64;
            default: throw new FormatException("Invalid Base64 string.");
        }
    }
}
}

