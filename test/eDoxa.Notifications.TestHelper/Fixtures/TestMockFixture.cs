// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Notifications.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

using Moq;

namespace eDoxa.Notifications.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        public TestMockFixture()
        {
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
            UserRepository = new Mock<IUserRepository>();
            EmailService = new Mock<IEmailService>();
            RedirectService = new Mock<IRedirectService>();
            UserService = new Mock<IUserService>();
        }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<IUserRepository> UserRepository { get; }
        
        public Mock<IEmailService> EmailService { get; }

        public Mock<IRedirectService> RedirectService { get; }

        public Mock<IUserService> UserService { get; }
    }
}
