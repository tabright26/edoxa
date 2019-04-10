// Filename: IIdentityParserService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Common.ValueObjects;

namespace eDoxa.Seedwork.Application.Services
{
    public interface IIdentityParserService
    {
        Guid Subject();

        string Email();

        string PhoneNumber();

        Name Name();

        BirthDate BirthDate();

        Guid? GetClanId();

        string GetCustomerId();
    }
}