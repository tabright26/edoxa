// Filename: ICashierHttpContext.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.Security.Abstractions
{
    public interface ICashierHttpContext
    {
        UserId UserId { get; }
    }
}
