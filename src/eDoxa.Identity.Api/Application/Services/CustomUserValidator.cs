// Filename: CustomUserValidator.cs
// Date Created: 2020-02-01
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class CustomUserValidator : UserValidator<User>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            var errors = new List<IdentityError>();

            await this.ValidateEmail(manager, user, errors);

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        private async Task ValidateEmail(UserManager<User> manager, User user, List<IdentityError> errors)
        {
            var email = await manager.GetEmailAsync(user);

            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(Describer.InvalidEmail(email));

                return;
            }

            if (!new EmailAddressAttribute().IsValid(email))
            {
                errors.Add(Describer.InvalidEmail(email));

                return;
            }

            var owner = await manager.FindByEmailAsync(email);

            if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(Describer.DuplicateEmail(email));
            }
        }
    }
}
