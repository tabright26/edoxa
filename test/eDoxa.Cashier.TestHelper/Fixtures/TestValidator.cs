// Filename: TestValidator.cs
// Date Created: 2020-01-14
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Moq;

namespace eDoxa.Cashier.TestHelper.Fixtures
{
    public sealed class TestValidator
    {
        public TestValidator()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            var configuration = builder.Build();
            Options = configuration.Get<CashierAppSettings>();
        }

        private CashierAppSettings Options { get; }

        public IOptionsSnapshot<CashierAppSettings> OptionsWrapper
        {
            get
            {
                var mock = new Mock<IOptionsSnapshot<CashierAppSettings>>();

                mock.Setup(optionsSnapshot => optionsSnapshot.Value).Returns(Options);

                return mock.Object;
            }
        }
    }
}
