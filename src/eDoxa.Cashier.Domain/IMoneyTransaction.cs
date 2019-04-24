// Filename: IMoneyTransaction.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.Domain
{
    public interface IMoneyTransaction : ITransaction<Money>
    {
    }
}