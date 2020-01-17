// Filename: TestValidator.cs
// Date Created: 2020-01-14
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Moq;

namespace eDoxa.Identity.TestHelper.Fixtures
{
    public sealed class TestValidator
    {
        public TestValidator()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            var configuration = builder.Build();
            Options = configuration.GetSection("Api").Get<IdentityApiOptions>();
        }

        private IdentityApiOptions Options { get; }

        public IOptionsSnapshot<IdentityApiOptions> OptionsWrapper
        {
            get
            {
                var mock = new Mock<IOptionsSnapshot<IdentityApiOptions>>();

                mock.Setup(optionsSnapshot => optionsSnapshot.Value).Returns(Options);

                return mock.Object;
            }
        }
    }
}
