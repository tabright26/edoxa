// Filename: UserDTO.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Identity.DTO
{
    [JsonObject]
    public class UserDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("tag")]
        public short Tag { get; set; }

        [JsonProperty("currentStatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UserStatus CurrentStatus { get; set; }

        [JsonProperty("previousStatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UserStatus PreviousStatus { get; set; }

        [JsonProperty("statusChanged")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StatusChanged { get; set; }
    }
}