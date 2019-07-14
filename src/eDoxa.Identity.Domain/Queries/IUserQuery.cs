// Filename: IUserQuery.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Domain.Queries
{
    public interface IUserQuery
    {
        Task<IReadOnlyCollection<User>> FetchUsersAsync();
    }
}
