// Filename: PhoneNumberValidator.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class PhoneNumberValidator : IUserValidator<User>
    {
        public PhoneNumberValidator(CustomIdentityErrorDescriber errors = null)
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

            await this.ValidateAsync(manager, user, errors);

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        private async Task ValidateAsync(UserManager<User> manager, User user, List<IdentityError> errors)
        {
            var phoneNumber = await manager.GetPhoneNumberAsync(user);

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
            }
        }
    }
}
