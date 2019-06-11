﻿using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
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
                    ruleSet.CustomInstantiator(faker => new ExternalAccount(faker.Random.Guid().ToString().Replace("-", string.Empty)));
                }
            );
        }

        public ExternalAccount FakeExternalAccount(Game game)
        {
            return this.Generate(game.ToString());
        }
    }
}
