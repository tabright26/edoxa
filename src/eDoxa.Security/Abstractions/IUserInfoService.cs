// Filename: IUserInfoService.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

namespace eDoxa.Security.Abstractions
{
    public interface IUserInfoService
    {
        string Subject { get; }

        string PreferredUserName { get; }

        string GivenName { get; }

        string FamilyName { get; }

        string Name { get; }

        string BirthDate { get; }

        string Email { get; }

        string EmailVerified { get; }

        string PhoneNumber { get; }

        string PhoneNumberVerified { get; }

        string Address { get; }

        string StripeAccountId { get; }

        string StripeBankAccountId { get; }

        string StripeCustomerId { get; }

        IEnumerable<string> Roles { get; }

        IEnumerable<string> Permissions { get; }
    }
}