// Filename: CustomUserManager.cs
// Date Created: 2019-07-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Managers
{
    public sealed class CustomUserManager : UserManager<UserModel>
    {
        private static readonly Random Random = new Random();

        public CustomUserManager(
            IUserStore<UserModel> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserModel> passwordHasher,
            IEnumerable<IUserValidator<UserModel>> userValidators,
            IEnumerable<IPasswordValidator<UserModel>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<CustomUserManager> logger
        ) : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger
        )
        {
        }

        private static int GenerateUniqueTag(IReadOnlyCollection<int> tags)
        {
            while (true)
            {
                var tag = GenerateTag();

                if (tags.Contains(tag))
                {
                    continue;
                }

                return tag;
            }
        }

        private static int GenerateTag()
        {
            return Random.Next(100, 10000);
        }

        private static string BuildGamertag(string userName, int tag)
        {
            return $"{userName}#{tag}";
        }

        [NotNull]
        [ItemNotNull]
        public override async Task<IdentityResult> SetUserNameAsync([NotNull] UserModel userModel, [NotNull] string userName)
        {
            var tags = await this.GetExistingTags(userName);

            var tag = tags.Any() ? GenerateUniqueTag(tags) : GenerateTag();

            return await base.SetUserNameAsync(userModel, BuildGamertag(userName, tag));
        }

        private async Task<IReadOnlyCollection<int>> GetExistingTags(string userName)
        {
            return await Users.Where(user => user.UserName.Contains(userName)).Select(user => user.UserName.Split('#', StringSplitOptions.None).Last()).Cast<int>().ToListAsync();
        }
    }
}
