// Filename: CustomIdentityErrorDescriber.cs
// Date Created: 2019-07-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public class CustomIdentityErrorDescriber : Microsoft.AspNetCore.Identity.IdentityErrorDescriber
    {
        public IdentityError GameProviderAlreadyLinked()
        {
            return new IdentityError
            {
                Code = nameof(this.GameProviderAlreadyLinked),
                Description = "The user's game provider is already linked."
            };
        }

        public IdentityError GameProviderAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(this.GameProviderAlreadyAssociated),
                Description = "A user with this game provided already exists."
            };
        }

        public IdentityError GameProviderUnlinked()
        {
            return new IdentityError
            {
                Code = nameof(this.GameProviderUnlinked),
                Description = "The user game provider is unlinked."
            };
        }

        public IdentityError InvalidUserName()
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = "The hashtag (#) character is not allowed in the user name."
            };
        }
    }
}
