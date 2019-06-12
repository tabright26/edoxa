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
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class MatchFaker : CustomFaker<Match>
    {
        private readonly MatchReferenceFaker _matchReferenceFaker = new MatchReferenceFaker();
        private readonly MatchStatsFaker _matchStatsFaker = new MatchStatsFaker();
        private readonly ScoringFaker _scoringFaker = new ScoringFaker();

        public MatchFaker()
        {
            this.CustomInstantiator(
                _ => new Match(_matchReferenceFaker.FakeMatchReference(Game), _matchStatsFaker.FakeMatchStats(Game), _scoringFaker.FakeMatchStats(Game))
            );
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
