// Filename: BankAccountDTO.cs
// Date Created: 2019-05-11
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
    public class BankAccountDTO
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("bank_name")] public string BankName { get; set; }

        [JsonProperty("last4")] public string Last4 { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}