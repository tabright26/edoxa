// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Sendgrid;
using eDoxa.Sendgrid.Services.Abstractions;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Moq;

namespace eDoxa.Notifications.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        private static readonly Lazy<SendgridOptions> _sendgridOptions = new Lazy<SendgridOptions>(
            () =>
            {
                var builder = new ConfigurationBuilder();

                builder.AddJsonFile("appsettings.json", false);

                var configuration = builder.Build();

                return configuration.GetSection("Sendgrid").Get<SendgridOptions>();
            });

        public TestMockFixture()
        {
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
            UserRepository = new Mock<IUserRepository>();
            EmailService = new Mock<ISendgridService>();
            RedirectService = new Mock<IRedirectService>();
            UserService = new Mock<IUserService>();
        }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<IUserRepository> UserRepository { get; }

        public Mock<ISendgridService> EmailService { get; }

        public Mock<IRedirectService> RedirectService { get; }

        public Mock<IUserService> UserService { get; }

        public Mock<IOptionsSnapshot<SendgridOptions>> SendgridOptions
        {
            get
            {
                var mock = new Mock<IOptionsSnapshot<SendgridOptions>>();

                mock.Setup(snapshot => snapshot.Value).Returns(_sendgridOptions.Value);

                return mock;
            }
        }
    }
}
