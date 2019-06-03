// Filename: PayoutDTO.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonObject]
    public class PayoutDTO
    {
        [CanBeNull]
        [JsonProperty("currency")]
        public CurrencyType Currency { get; set; }

        [JsonProperty("buckets")]
        public BucketDTO[] Buckets { get; set; }
    }
}
