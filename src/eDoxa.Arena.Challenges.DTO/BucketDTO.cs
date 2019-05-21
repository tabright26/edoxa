// Filename: BucketDTO.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonObject]
    public class BucketDTO
    {
        [JsonProperty("prize")]
        public decimal Prize { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
