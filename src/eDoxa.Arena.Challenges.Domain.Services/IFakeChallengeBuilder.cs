// Filename: IChallengeBuilder.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public interface IFakeChallengeBuilder
    {
        Challenge Build();

        void RegisterParticipants();

        void SnapshotParticipantMatches();
    }
}
