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

        Task<DomainValidationResult<User>> CreateUserAsync(UserId userId, string email);

        Task<DomainValidationResult<User>> UpdateUserAsync(User user, string email);

        Task SendEmailAsync(UserId userId, string templateId, object templateData);
    }
}
