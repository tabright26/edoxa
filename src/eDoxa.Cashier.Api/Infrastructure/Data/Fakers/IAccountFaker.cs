// Filename: IAccountFaker.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public interface IAccountFaker
    {
        IAccount FakeAccount(string? ruleSets = null);
    }
}
