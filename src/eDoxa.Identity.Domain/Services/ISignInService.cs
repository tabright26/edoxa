// Filename: ISignInService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.Services
{
    public interface ISignInService
    {
        Task SignOutAsync();

        Task<SignInResult> PasswordSignInAsync(
            User user,
            string password,
            bool isPersistent,
            bool lockoutOnFailure
        );
    }
}
