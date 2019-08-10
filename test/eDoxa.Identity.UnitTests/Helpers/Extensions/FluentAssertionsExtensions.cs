// Filename: FluentAssertionsExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Seedwork.Domain.Extensions;

using FluentAssertions;

namespace eDoxa.Identity.UnitTests.Helpers.Extensions
{
    public static class FluentAssertionsExtensions
    {
        public static void AssertStateIsValid(this IEnumerable<UserResponse> users)
        {
            users.ForEach(AssertStateIsValid);
        }

        public static void AssertStateIsValid(this UserResponse user)
        {
            user.Should().NotBeNull();
        }
    }
}
