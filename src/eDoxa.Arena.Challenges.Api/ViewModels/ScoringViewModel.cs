﻿// Filename: ScoringViewModel.cs
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

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonDictionary]
    public class ScoringViewModel : Dictionary<string, float>
    {
        public ScoringViewModel(Scoring scoring) : base(scoring.ToDictionary(pair => pair.Key.ToString(), pair => Convert.ToSingle((float) pair.Value)))
        {
        }
    }
}
