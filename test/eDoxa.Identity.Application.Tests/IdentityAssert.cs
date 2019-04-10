// Filename: IdentityAssert.cs
// Date Created: 2019-04-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.DTO;

using FluentAssertions;

namespace eDoxa.Identity.Application.Tests
{
    internal static class IdentityAssert
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