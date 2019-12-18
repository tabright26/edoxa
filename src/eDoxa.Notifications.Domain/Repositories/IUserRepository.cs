// Filename: IUserRepository.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Notifications.Domain.Repositories
{
    public interface IUserRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<bool> UserExistsAsync(UserId userId);

        Task<User> FindUserAsync(UserId userId);

        void Create(User user);
    }
}
