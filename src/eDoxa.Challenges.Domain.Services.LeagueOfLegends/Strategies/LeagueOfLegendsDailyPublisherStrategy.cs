using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies
{
    internal sealed class LeagueOfLegendsDailyPublisherStrategy : IPublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge("Daily 1", ChallengeInterval.Daily);
                yield return new LeagueOfLegendsChallenge("Daily 2", ChallengeInterval.Daily);
                yield return new LeagueOfLegendsChallenge("Daily 3", ChallengeInterval.Daily);
            }
        }
    }
}
