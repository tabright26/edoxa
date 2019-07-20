// Filename: CustomUserValidator.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Application.Validators
{
    public sealed class CustomUserValidator : IUserValidator<User>
    {
        [NotNull]
        [ItemNotNull]
        public Task<IdentityResult> ValidateAsync([NotNull] UserManager<User> manager, [NotNull] User user)
        {
            if (user.UserName.Count(c => c == '#') > 1)
            {
                return Task.FromResult(IdentityResult.Failed(InvalidGamertag()));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        private static IdentityError InvalidGamertag()
        {
            return new IdentityError
            {
                Code = nameof(InvalidGamertag),
                Description = "The hashtag (#) character is not allowed in the gamertag."
            };
        }
    }
}
