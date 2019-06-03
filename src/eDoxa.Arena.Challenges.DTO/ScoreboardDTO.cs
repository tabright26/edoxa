// Filename: ScoreboardDTO.cs
// Date Created: 2019-06-01
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

using eDoxa.Arena.Challenges.Domain.AggregateModels;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonDictionary]
    public class ScoreboardDTO : Dictionary<Guid, decimal?>
    {
        public ScoreboardDTO(Scoreboard scoreboard) : base(
            scoreboard.ToDictionary(
                participant => participant.Key.ToGuid(),
                participant => participant.Value != null ? (decimal?) participant.Value.Value : null
            )
        )
        {
        }
    }
}
