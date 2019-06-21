using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    internal sealed class Challenge : DataSet
    {
        public ChallengeId Id()
        {
            return ChallengeId.FromGuid(Random.Guid());
        }

        public ChallengeName Name()
        {
            return new ChallengeName(nameof(Domain.AggregateModels.ChallengeAggregate.Challenge));
        }

        public ChallengeGame Game()
        {
            return ChallengeGame.LeagueOfLegends;
        }

        //public ChallengeState State()
        //{

        //}

        //public ChallengeSetup Setup()
        //{
        //    return new ChallengeSetup();
        //}

        //public ChallengeDuration Duration()
        //{
        //}

        //public ChallengeTimeline Timeline()
        //{
        //}


    }
}
