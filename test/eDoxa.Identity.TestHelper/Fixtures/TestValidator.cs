// Filename: TestValidator.cs
// Date Created: 2020-01-14
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.TestHelper.Fixtures
{
    public sealed class TestValidator
    {
        public TestValidator()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            var configuration = builder.Build();
            OptionsWrapper = new OptionsWrapper<IdentityApiOptions>(configuration.GetSection("Api").Get<IdentityApiOptions>());
        }

        public OptionsWrapper<IdentityApiOptions> OptionsWrapper { get; }
    }
}
