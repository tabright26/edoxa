// Filename: EmailValidator.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public sealed class EmailValidator : IUserValidator<User>
    {
        public EmailValidator(CustomIdentityErrorDescriber errors = null)
        {
            Describer = errors ?? new CustomIdentityErrorDescriber();
        }

        public CustomIdentityErrorDescriber Describer { get; private set; }

        [NotNull]
        [ItemNotNull]
        public async Task<IdentityResult> ValidateAsync([NotNull] UserManager<User> manager, [NotNull] User user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var errors = new List<IdentityError>();

            if (manager.Options.User.RequireUniqueEmail)
            {
                await this.ValidateAsync(manager, user, errors);
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        private async Task ValidateAsync(UserManager<User> manager, User user, List<IdentityError> errors)
        {
            var email = await manager.GetEmailAsync(user);

            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(Describer.InvalidEmail(email));
            }
            else if (!new EmailAddressAttribute().IsValid(email))
            {
                errors.Add(Describer.InvalidEmail(email));
            }
            else
            {
                var byEmailAsync = await manager.FindByEmailAsync(email);

                var flag = byEmailAsync != null;

                if (flag)
                {
                    var userId = await manager.GetUserIdAsync(byEmailAsync);

                    flag = !string.Equals(userId, await manager.GetUserIdAsync(user));
                }

                if (!flag)
                {
                    return;
                }

                errors.Add(Describer.DuplicateEmail(email));
            }
        }
    }
}
