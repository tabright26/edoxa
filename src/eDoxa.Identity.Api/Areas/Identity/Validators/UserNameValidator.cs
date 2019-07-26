// Filename: UserNameValidator.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public sealed class UserNameValidator : IUserValidator<User>
    {
        public UserNameValidator(CustomIdentityErrorDescriber errors = null)
        {
            Describer = errors ?? new CustomIdentityErrorDescriber();
        }

        public CustomIdentityErrorDescriber Describer { get; private set; }

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

        private async Task ValidateAsync(UserManager<User> manager, User user, ICollection<IdentityError> errors)
        {
            var userName = await manager.GetUserNameAsync(user);

            if (string.IsNullOrWhiteSpace(userName))
            {
                return;
            }

            if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) &&
                userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains<char>(c)))
            {
                errors.Add(Describer.InvalidUserName(userName));
            }
            else if (user.UserName.Count(c => c == '#') > 1)
            {
                errors.Add(Describer.InvalidUserName());
            }
        }
    }
}
