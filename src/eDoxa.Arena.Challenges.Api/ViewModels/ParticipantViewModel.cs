// Filename: ParticipantViewModel.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class ParticipantViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("averageScore")]
        public decimal? AverageScore { get; set; }

        [JsonProperty("matches")]
        public MatchViewModel[] Matches { get; set; }
    }
}
