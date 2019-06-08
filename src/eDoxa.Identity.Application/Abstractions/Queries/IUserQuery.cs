// Filename: IUserQueries.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Application.ViewModels;

namespace eDoxa.Identity.Application.Abstractions.Queries
{
    public interface IUserQuery
    {
        Task<IReadOnlyCollection<UserViewModel>> FindUsersAsync();
    }
}