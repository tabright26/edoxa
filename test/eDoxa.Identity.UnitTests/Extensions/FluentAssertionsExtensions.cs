// Filename: IdentityExtensions.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Identity.Api.ViewModels;
using eDoxa.Seedwork.Domain.Extensions;

using FluentAssertions;

namespace eDoxa.Identity.UnitTests.Extensions
{
    public static class FluentAssertionsExtensions
    {
        public static void AssertStateIsValid(this IEnumerable<UserViewModel> users)
        {
            users.ForEach(AssertStateIsValid);
        }

        public static void AssertStateIsValid(this UserViewModel user)
        {
            user.Should().NotBeNull();
        }
    }
}
