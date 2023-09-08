// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="JsonSerializer.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;
using Newtonsoft.Json;

namespace MoneybaseTask.Common.Core.Infrastructure.Serialization;

public class JsonSerializer : IJsonSerializer
{
    public T Deserialize<T>(string source)
    {
        return JsonConvert.DeserializeObject<T>(source);
    }

    public string Serialize(object source, Formatting formatting = Formatting.Indented)
    {
        return JsonConvert.SerializeObject(source, formatting);
    }

    public string SerializeAndEncode(object source)
    {
        var objectToString = Serialize(source);

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(objectToString));
    }

    public T DecodeAndDeserialize<T>(string source)
    {
        var base64EncodedBytes = Convert.FromBase64String(source);
        var jsonObject = Encoding.UTF8.GetString(base64EncodedBytes);

        return Deserialize<T>(jsonObject);
    }
}