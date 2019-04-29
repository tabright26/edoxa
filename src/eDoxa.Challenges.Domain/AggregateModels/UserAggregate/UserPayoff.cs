// Filename: UserPrizes.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.UserAggregate
{
    public sealed class UserPayoff : Dictionary<UserId, Prize>, IUserPayoff
    {
    }
}