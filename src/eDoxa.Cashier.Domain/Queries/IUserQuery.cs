// Filename: IUserQuery.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface IUserQuery
    {
        IMapper Mapper { get; }

        [ItemCanBeNull]
        Task<User> FindUserAsync(UserId userId);
    }
}
