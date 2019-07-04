// Filename: WebApplicationFactory.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Testing;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    internal sealed class WebApplicationFactory<TStartup> : WebApplicationFactory<CashierDbContext, TStartup, Program>
    where TStartup : class
    {
    }
}
