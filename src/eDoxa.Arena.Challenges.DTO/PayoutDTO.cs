// Filename: PayoutDTO.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonObject]
    public class PayoutDTO
    {
        [JsonProperty("prizeType")]
        public Currency PrizeType { get; set; }

        [JsonProperty("buckets")]
        public BucketListDTO Buckets { get; set; }
    }
}
