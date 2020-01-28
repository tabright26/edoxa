// Filename: TestValidator.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Challenges.Options;

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
            Options = configuration.GetSection("Api").Get<ChallengesApiOptions>();
        }

        public ChallengesApiOptions Options { get; }

        public IOptionsSnapshot<ChallengesApiOptions> OptionsWrapper
        {
            get
            {
                var mock = new Mock<IOptionsSnapshot<ChallengesApiOptions>>();

                mock.Setup(snapshot => snapshot.Value).Returns(Options);

                return mock.Object;
            }
        }
    }
}
