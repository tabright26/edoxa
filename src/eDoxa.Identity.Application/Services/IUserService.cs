// Filename: IUserService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Application.Services
{
    public interface IUserService
    {
        Task ChangeStatusAsync(Guid userId, UserStatus status);

        Task<bool> UserExistsAsync(Guid userId);
    }
}