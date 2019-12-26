// Filename: TestValidator.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.Application;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace eDoxa.Challenges.TestHelper.Fixtures
{
    public sealed class TestValidator
    {
        public TestValidator()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            var configuration = builder.Build();
            Options = configuration.GetSection("Challenge").Get<ChallengeOptions>();
        }

        public ChallengeOptions Options { get; }

        public OptionsWrapper<ChallengeOptions> OptionsWrapper => new OptionsWrapper<ChallengeOptions>(Options);
    }
}
