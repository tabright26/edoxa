// Filename: TestValidator.cs
// Date Created: 2020-01-14
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.Options;

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
            Options = configuration.GetSection("Api").Get<CashierApiOptions>();
        }

        private CashierApiOptions Options { get; }

        public IOptionsSnapshot<CashierApiOptions> OptionsWrapper
        {
            get
            {
                var mock = new Mock<IOptionsSnapshot<CashierApiOptions>>();

                mock.Setup(optionsSnapshot => optionsSnapshot.Value).Returns(Options);

                return mock.Object;
            }
        }
    }
}
