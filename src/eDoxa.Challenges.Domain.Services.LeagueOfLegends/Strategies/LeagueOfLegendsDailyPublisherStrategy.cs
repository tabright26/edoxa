using System.Collections.Generic;

using eDoxa.Challenges.Domain.Entities;
using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies
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
