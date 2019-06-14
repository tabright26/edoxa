// Filename: MatchFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class MatchFaker : CustomFaker<Match>
    {
        public MatchFaker()
        {
            this.CustomInstantiator(faker => new Match(faker.MatchReference(Game), faker.MatchStats(Game), faker.Scoring(Game)));

            this.RuleFor(match => match.Id, faker => faker.MatchId());

            this.RuleFor(match => match.Timestamp, faker => faker.MatchTimestamp());
        }

        private Game Game { get; set; }

        public IEnumerable<Match> FakeMatches(int count, Game game)
        {
            Game = game;

            return this.Generate(count);
        }

        public Match FakeMatch(Game game)
        {
            Game = game;

            return this.Generate();
        }
    }
}
