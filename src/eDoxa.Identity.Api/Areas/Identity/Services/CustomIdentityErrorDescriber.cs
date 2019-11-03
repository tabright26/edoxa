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
