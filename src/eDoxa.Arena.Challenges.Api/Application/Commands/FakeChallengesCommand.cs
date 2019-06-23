// Filename: FakeChallengesCommand.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Commands.Abstractions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Api.Application.Commands
{
    [DataContract]
    public class FakeChallengesCommand : Command
    {
        public FakeChallengesCommand(
            int count,
            int seed,
            ChallengeGame game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null
        ) : this()
        {
            Count = count;
            Seed = seed;
            Game = game;
            State = state;
            EntryFeeCurrency = entryFeeCurrency;
        }

        public FakeChallengesCommand()
        {
            // Required for unit tests.
        }

        [DataMember(Name = "count")]
        public int Count { get; private set; }

        [DataMember(Name = "seed")]
        public int Seed { get; private set; }

        [DataMember(Name = "game", IsRequired = false)]
        public ChallengeGame Game { get; private set; }

        [DataMember(Name = "state", IsRequired = false)]
        public ChallengeState State { get; private set; }

        [DataMember(Name = "entryFeeCurrency", IsRequired = false)]
        public CurrencyType EntryFeeCurrency { get; private set; }
    }
}
