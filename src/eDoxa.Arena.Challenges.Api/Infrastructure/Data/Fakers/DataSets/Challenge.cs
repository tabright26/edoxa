using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;

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

        public Game Game()
        {
            return Seedwork.Common.Enumerations.Game.LeagueOfLegends;
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
