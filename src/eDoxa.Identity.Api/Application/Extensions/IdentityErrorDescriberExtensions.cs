// Filename: IdentityErrorDescriberExtensions.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Application.Extensions
{
    public static class IdentityErrorDescriberExtensions
    {
        public static IdentityError GameProviderAlreadyLinked(this IdentityErrorDescriber _)
        {
            return new IdentityError
            {
                Code = nameof(GameProviderAlreadyLinked),
                Description = "The user's game provider is already linked."
            };
        }

        public static IdentityError GameProviderAlreadyAssociated(this IdentityErrorDescriber _)
        {
            return new IdentityError
            {
                Code = nameof(GameProviderAlreadyAssociated),
                Description = "A user with this game provided already exists."
            };
        }

        public static IdentityError GameProviderUnlinked(this IdentityErrorDescriber _)
        {
            return new IdentityError
            {
                Code = nameof(GameProviderUnlinked),
                Description = "The user game provider is unlinked."
            };
        }
    }
}
