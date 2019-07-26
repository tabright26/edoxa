// Filename: CustomIdentityErrorDescriber.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public IdentityError GameAlreadyLinked()
        {
            return new IdentityError
            {
                Code = nameof(this.GameAlreadyLinked),
                Description = "The game is already linked to the user."
            };
        }

        public IdentityError GameAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(this.GameAlreadyAssociated),
                Description = "The game is already associated with a user."
            };
        }

        public IdentityError GameNotAssociated()
        {
            return new IdentityError
            {
                Code = nameof(this.GameNotAssociated),
                Description = "The game is not associated with the user."
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
