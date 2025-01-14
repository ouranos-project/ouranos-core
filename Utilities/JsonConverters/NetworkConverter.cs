﻿using System;
using Martiscoin.Networks;
using Newtonsoft.Json;

namespace Martiscoin.Utilities.JsonConverters
{
    /// <summary>
    /// Converter used to convert <see cref="Network"/> to and from JSON.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class NetworkConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Network);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return NetworkHelpers.GetNetwork((string)reader.Value);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Network)value).ToString());
        }
    }
}
