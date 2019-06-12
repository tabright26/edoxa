// Filename: ExternalAccountFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ExternalAccountFaker : CustomFaker<ExternalAccount>
    {
        public ExternalAccountFaker()
        {
            this.RuleSet(
                Game.LeagueOfLegends.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(faker => new ExternalAccount($"LeagueOfLegends{faker.Random.Guid().ToString().Replace("-", string.Empty).Substring(10)}"));
                }
            );
        }

        public ExternalAccount FakeExternalAccount(Game game)
        {
            return this.Generate(game.ToString());
        }
    }
}
