// Filename: IUserQuery.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.ViewModels;

namespace eDoxa.Identity.Api.Application.Abstractions
{
    public interface IUserQuery
    {
        Task<IReadOnlyCollection<UserViewModel>> FindUsersAsync();
    }
}
