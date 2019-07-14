// Filename: IUserRepository.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        void Create(IEnumerable<User> user);

        void Create(User user);

        Task<User> FindUserAsync();

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
