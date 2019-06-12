// Filename: CreateChallengeCommand.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Commands.Abstractions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Api.Application.Commands
{
    [DataContract]
    public class FakeChallengesCommand : Command<IEnumerable<ChallengeViewModel>>
    {
        public FakeChallengesCommand(
            int count,
            int? seed = null,
            Game game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null
        )
        {
            Count = count;
            Seed = seed;
            Game = game;
            State = state;
            EntryFeeCurrency = entryFeeCurrency;
        }

        [DataMember(Name = "count")]
        public int Count { get; private set; }

        [DataMember(Name = "seed", IsRequired = false)]
        public int? Seed { get; private set; }

        [DataMember(Name = "game", IsRequired = false)]
        public Game Game { get; private set; }

        [DataMember(Name = "state", IsRequired = false)]
        public ChallengeState State { get; private set; }

        [DataMember(Name = "entryFeeCurrency", IsRequired = false)]
        public CurrencyType EntryFeeCurrency { get; private set; }
    }
}
