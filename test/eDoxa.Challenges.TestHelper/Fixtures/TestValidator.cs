// Filename: TestValidator.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.Application;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Moq;

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

        public IOptionsSnapshot<ChallengeOptions> OptionsWrapper
        {
            get
            {
                var mock = new Mock<IOptionsSnapshot<ChallengeOptions>>();

                mock.Setup(snapshot => snapshot.Value).Returns(Options);

                return mock.Object;
            }
        }
    }
}
