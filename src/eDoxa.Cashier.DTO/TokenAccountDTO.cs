// Filename: TokenAccountDTO.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Cashier.DTO
{
    [JsonObject]
    public class TokenAccountDTO
    {
        [JsonProperty("balance")] public long Balance { get; set; }

        [JsonProperty("pending")] public long Pending { get; set; }
    }
}