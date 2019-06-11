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

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class MatchFaker : CustomFaker<Match>
    {
        private readonly MatchReferenceFaker _matchReferenceFaker = new MatchReferenceFaker();
        private readonly MatchStatsFaker _matchStatsFaker = new MatchStatsFaker();
        private readonly ScoringFaker _scoringFaker = new ScoringFaker();

        private Game _game;

        public MatchFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var game = _game ?? faker.PickRandom(Game.GetAll());

                    return new Match(_matchReferenceFaker.FakeMatchReference(game), _matchStatsFaker.FakeMatchStats(game), _scoringFaker.FakeScoring(game));
                }
            );
        }

        [NotNull]
        public override Faker<Match> UseSeed(int seed)
        {
            var challengeFaker = base.UseSeed(seed);

            _matchReferenceFaker.UseSeed(seed);

            _matchStatsFaker.UseSeed(seed);

            _scoringFaker.UseSeed(seed);

            return challengeFaker;
        }

        public IEnumerable<Match> FakeMatches(int count, Game game = null)
        {
            _game = game;

            return this.Generate(count);
        }

        public Match FakeMatch(Game game = null)
        {
            _game = game;

            return this.Generate();
        }
    }
}
