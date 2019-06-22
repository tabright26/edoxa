// Filename: ChallengeDataSet.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    public class ChallengeDataSet : DataSet
    {
        public ChallengeDataSet(string local = "en") : base(local)
        {
            Timeline = Notifier.Flow(new ChallengeTimelineDataSet());
            Setup = Notifier.Flow(new ChallengeSetupDataSet());
        }

        public ChallengeTimelineDataSet Timeline { get; }

        public ChallengeSetupDataSet Setup { get; }

        public ChallengeId Id()
        {
            return ChallengeId.FromGuid(Random.Guid());
        }

        public ChallengeName Name()
        {
            return new ChallengeName(Random.CollectionItem(new[] {nameof(Challenge)}));
        }

        public ChallengeGame Game()
        {
            return Random.CollectionItem(ChallengeGame.GetEnumerations().ToList());
        }

        //public ChallengeState State()
        //{

        //}

        //public ChallengeSetup Setup()
        //{
        //    return new ChallengeSetup();
        //}

        //public ChallengeTimeline Timeline()
        //{
        //}
    }
}
