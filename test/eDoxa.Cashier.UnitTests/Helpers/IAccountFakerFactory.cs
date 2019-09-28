// Filename: IAccountFakerFactory.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;

namespace eDoxa.Cashier.UnitTests.Helpers
{
    public interface IAccountFakerFactory
    {
        IAccountFaker CreateFaker(int? seed);
    }
}
