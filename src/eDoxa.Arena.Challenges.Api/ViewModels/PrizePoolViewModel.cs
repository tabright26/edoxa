﻿// Filename: PrizePoolViewModel.cs
// Date Created: 2019-06-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.Enumerations;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class PrizePoolViewModel
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public CurrencyType Type { get; set; }
    }
}
