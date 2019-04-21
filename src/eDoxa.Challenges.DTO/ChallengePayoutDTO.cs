// Filename: ChallengePayoutDTO.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Challenges.DTO
{
    [JsonObject]
    public class ChallengePayoutDTO
    {       
        [JsonProperty("buckets")] public BucketListDTO Buckets { get; set; }
    }
}