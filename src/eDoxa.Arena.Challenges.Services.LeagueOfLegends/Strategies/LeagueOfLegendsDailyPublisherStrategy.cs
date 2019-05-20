using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Strategies
{
    internal sealed class LeagueOfLegendsDailyPublisherStrategy : IPublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Daily 1"), PublisherInterval.Daily);
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Daily 2"), PublisherInterval.Daily);
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Daily 3"), PublisherInterval.Daily);
            }
        }
    }
}
