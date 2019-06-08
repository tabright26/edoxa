// Filename: TestModeViewModel.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Application.ViewModels
{
    [JsonObject]
    public class TestModeViewModel
    {
        [JsonProperty("state")]
        public ChallengeState State { get; set; }

        [JsonProperty("averageBestOf")]
        public TestModeMatchQuantity MatchQuantity { get; set; }

        [JsonProperty("participantQuantity")]
        public TestModeParticipantQuantity ParticipantQuantity { get; set; }
    }
}
