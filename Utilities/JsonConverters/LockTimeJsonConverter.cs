﻿using System;
using Martiscoin.NBitcoin;
using Newtonsoft.Json;

namespace Martiscoin.Utilities.JsonConverters
{
    /// <summary>
    /// Converter used to convert a <see cref="LockTime"/> to and from JSON.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class LockTimeJsonConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LockTime);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return reader.TokenType == JsonToken.Null ? LockTime.Zero : new LockTime((uint)reader.Value);
            }
            catch
            {
                throw new JsonObjectException("Invalid locktime", reader);
            }
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(((LockTime)value).Value);
            }
        }
    }
}
