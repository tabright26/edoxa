// Filename: ScoreboardViewModel.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Domain.ViewModels
{
    [JsonDictionary]
    public class ScoreboardViewModel : Dictionary<Guid, decimal?>
    {
        public ScoreboardViewModel(Scoreboard scoreboard) : base(
            scoreboard.ToDictionary(
                participant => participant.Key.ToGuid(),
                participant => participant.Value != null ? (decimal?) participant.Value : null
            )
        )
        {
        }

        [JsonConstructor]
        public ScoreboardViewModel()
        {
            
        }
    }
}
