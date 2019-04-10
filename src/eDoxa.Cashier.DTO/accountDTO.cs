// Filename: AccountDTO.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Cashier.DTO
{
    [JsonObject]
    public class AccountDTO
    {
        [JsonProperty("funds")]
        public CurrencyDTO Funds { get; set; }

        [JsonProperty("tokens")]
        public CurrencyDTO Tokens { get; set; }
    }
}