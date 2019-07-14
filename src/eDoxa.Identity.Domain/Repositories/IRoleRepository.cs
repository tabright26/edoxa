// Filename: IRoleRepository.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;

namespace eDoxa.Identity.Domain.Repositories
{
    public interface IRoleRepository
    {
        void Create(IEnumerable<Role> roles);

        void Create(Role role);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
