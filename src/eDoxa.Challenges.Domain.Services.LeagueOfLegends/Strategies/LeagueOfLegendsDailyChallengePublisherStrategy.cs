using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies
{
    internal sealed class LeagueOfLegendsDailyChallengePublisherStrategy : IChallengePublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge("Daily 1", ChallengePublisherPeriodicity.Daily);
                yield return new LeagueOfLegendsChallenge("Daily 2", ChallengePublisherPeriodicity.Daily);
                yield return new LeagueOfLegendsChallenge("Daily 3", ChallengePublisherPeriodicity.Daily);
            }
        }
    }
}
