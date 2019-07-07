// Filename: IParticipantPrizes.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public interface IParticipantPrizes : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
