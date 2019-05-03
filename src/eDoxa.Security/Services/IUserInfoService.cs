// Filename: IUserInfoService.cs
// Date Created: 2019-05-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Functional.Maybe;

namespace eDoxa.Security.Services
{
    public interface IUserInfoService
    {
        Option<Guid> Subject { get; }

        Option<string> CustomerId { get; }
    }
}