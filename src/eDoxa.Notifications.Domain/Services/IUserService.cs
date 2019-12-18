// Filename: IUserService.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Notifications.Domain.Services
{
    public interface IUserService
    {
        Task<bool> UserExistsAsync(UserId userId);

        Task<User> FindUserAsync(UserId userId);

        Task<IDomainValidationResult> CreateUserAsync(UserId userId, string email);

        Task<IDomainValidationResult> UpdateUserAsync(User user, string email);

        Task SendEmailAsync(UserId userId, string subject, string htmlContent);
    }
}
