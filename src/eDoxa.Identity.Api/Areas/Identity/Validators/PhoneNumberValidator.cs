// Filename: PhoneNumberValidator.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class PhoneNumberValidator : IUserValidator<User>
    {
        public PhoneNumberValidator(CustomIdentityErrorDescriber errors)
        {
            Describer = errors;
        }

        public CustomIdentityErrorDescriber Describer { get; }

        public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
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
