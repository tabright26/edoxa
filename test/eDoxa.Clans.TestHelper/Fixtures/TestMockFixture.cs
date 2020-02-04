// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

using Moq;

namespace eDoxa.Clans.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        public TestMockFixture()
        {
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
            CandidatureRepository = new Mock<ICandidatureRepository>();
            ClanRepository = new Mock<IClanRepository>();
            InvitationRepository = new Mock<IInvitationRepository>();
            CandidatureService = new Mock<ICandidatureService>();
            ClanService = new Mock<IClanService>();
            InvitationService = new Mock<IInvitationService>();
        }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<ICandidatureRepository> CandidatureRepository { get; }

        public Mock<IClanRepository> ClanRepository { get; }

        public Mock<IInvitationRepository> InvitationRepository { get; }

        public Mock<ICandidatureService> CandidatureService { get; }

        public Mock<IClanService> ClanService { get; }

        public Mock<IInvitationService> InvitationService { get; }
    }
}
