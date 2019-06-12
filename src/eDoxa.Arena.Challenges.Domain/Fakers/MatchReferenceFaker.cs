// Filename: MatchReferenceFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class MatchReferenceFaker : CustomFaker<MatchReference>
    {
        public MatchReferenceFaker()
        {
            this.RuleSet(
                Game.LeagueOfLegends.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(faker => new MatchReference(faker.Random.Long(1000000000, 9999999999)));
                }
            );
        }

        public MatchReference FakeMatchReference(Game game)
        {
            return this.Generate(game.ToString());
        }
    }
}
