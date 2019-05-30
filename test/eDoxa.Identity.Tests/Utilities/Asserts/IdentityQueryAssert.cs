// Filename: IdentityQueryAssert.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.DTO;

using FluentAssertions;

namespace eDoxa.Identity.Tests.Utilities.Asserts
{
    internal static class IdentityQueryAssert
    {
        public static void IsMapped(UserListDTO users)
        {
            users.Should().NotBeNull();

            foreach (var user in users)
            {
                IsMapped(user);
            }
        }

        public static void IsMapped(UserDTO user)
        {
            user.Should().NotBeNull();
        }
    }
}
