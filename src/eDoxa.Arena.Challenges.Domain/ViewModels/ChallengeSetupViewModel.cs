﻿// Filename: ChallengeSetupViewModel.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Domain.ViewModels
{
    [JsonObject]
    public class ChallengeSetupViewModel
    {
        [JsonProperty("bestOf")]
        public int BestOf { get; set; }

        [JsonProperty("entries")]
        public int Entries { get; set; }

        [JsonProperty("payoutEntries")]
        public int PayoutEntries { get; set; }

        [JsonProperty("entryFee")]
        public EntryFeeViewModel EntryFee { get; set; }
    }
}