// Filename: CashierWebAggregatorHostFactory.cs
// Date Created: 2019-12-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Web.Aggregator;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.FunctionalTests.TestHelper.Aggregators.Web.Cashier
{
    public sealed class CashierWebAggregatorHostFactory : WebHostFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseCustomContentRoot("TestHelper/Aggregators/Web/Cashier");
        }
    }
}
