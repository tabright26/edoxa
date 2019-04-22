using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class RandomChallenge : Challenge
    {
        internal RandomChallenge(Game game, ChallengeName name, ChallengePublisherPeriodicity periodicity) : base(game, name, new RandomChallengeSetup(periodicity), new ChallengeTimeline(periodicity))
        {
        }
    }
}
