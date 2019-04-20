using System;
using System.Collections.Generic;
using System.Text;

using eDoxa.Challenges.Domain.ValueObjects;

namespace eDoxa.Challenges.Domain.AggregateModels.UserAggregate
{
    public sealed class UserPrizes : Dictionary<UserId, Prize>, IUserPrizes
    {
    }
}
