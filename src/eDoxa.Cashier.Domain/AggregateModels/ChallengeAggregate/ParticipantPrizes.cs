// Filename: ParticipantPrizes.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ParticipantPrizes : Dictionary<UserId, Prize>, IParticipantPrizes
    {
    }
}
