// Filename: PaymentHostFactory.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Payment.Api;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.FunctionalTests.TestHelper.Services.Payment
{
    public sealed class PaymentHostFactory : WebHostFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseCustomContentRoot("TestHelper/Services/Payment");
        }
    }
}
