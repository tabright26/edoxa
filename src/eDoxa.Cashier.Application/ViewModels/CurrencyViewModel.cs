// Filename: CurrencyViewModel.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Application.ViewModels
{
    [JsonObject]
    public class CurrencyViewModel
    {
        [JsonProperty("type")]
        public CurrencyType Type { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
