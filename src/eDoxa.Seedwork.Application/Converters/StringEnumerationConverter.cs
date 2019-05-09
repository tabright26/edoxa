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

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Converters
{
    public sealed class StringEnumerationConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumeration enumeration)
            {
                writer.WriteValue(enumeration.ToString());
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Null:

                        return null;

                    case JsonToken.String:

                        return Enumeration.FromName(reader.Value.ToString(), objectType);

                    case JsonToken.Integer:

                        return Enumeration.FromValue((int) reader.Value, objectType);

                    default:

                        throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enumeration.");
                }
            }
            catch (Exception exception)
            {
                throw new JsonSerializationException($"Error converting value {reader.Value} to type '{objectType}'.", exception);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnumeration();
        }
    }
}