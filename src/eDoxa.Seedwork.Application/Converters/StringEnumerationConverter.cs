// Filename: StringEnumerationConverter.cs
// Date Created: 2019-05-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Utilities;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Converters
{
    public sealed class StringEnumerationConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Enumeration enumeration)
            {
                writer.WriteValue(enumeration.DisplayName);
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            try
            {
                //switch (reader.TokenType)
                //{
                //    case JsonToken.String:

                //        var displayName = reader.Value.ToString();

                //        return displayName != string.Empty ? Enumeration.FromDisplayName<TEnumeration>(displayName) : null;

                //    case JsonToken.Integer:

                //        return Enumeration.FromValue<TEnumeration>((int)reader.Value);

                //    default:

                //        throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enumeration.");
                //}
            }
            catch (Exception exception)
            {
                throw new JsonSerializationException($"Error converting value {reader.Value} to type '{objectType}'.", exception);
            }

            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enumeration.");
        }

        public override bool CanConvert(Type objectType)
        {
            return EnumerationUtils.IsDefined(objectType);
        }
    }

    public sealed class StringEnumerationConverter<TEnumeration> : JsonConverter<TEnumeration>
    where TEnumeration : Enumeration, new()
    {
        public override void WriteJson(JsonWriter writer, TEnumeration value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.DisplayName);
            }
        }

        public override TEnumeration ReadJson(JsonReader reader, Type objectType, TEnumeration existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            try
            {
                switch (reader.TokenType)
                {
                    case JsonToken.String:

                        var displayName = reader.Value.ToString();

                        return displayName != string.Empty ? Enumeration.FromDisplayName<TEnumeration>(displayName) : null;

                    case JsonToken.Integer:

                        return Enumeration.FromValue<TEnumeration>((int) reader.Value);

                    default:

                        throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enumeration.");
                }
            }
            catch (Exception exception)
            {
                throw new JsonSerializationException($"Error converting value {reader.Value} to type '{objectType}'.", exception);
            }
        }
    }
}