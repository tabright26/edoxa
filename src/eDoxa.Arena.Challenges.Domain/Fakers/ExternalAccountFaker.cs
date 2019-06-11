using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

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
            var externalAccount = this.Generate(game.ToString());

            Console.WriteLine(externalAccount.DumbAsJson());

            return externalAccount;
        }
    }
}
