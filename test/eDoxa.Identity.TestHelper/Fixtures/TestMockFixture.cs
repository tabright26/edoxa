// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

using IdentityServer4.Services;

using Moq;

namespace eDoxa.Identity.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        public TestMockFixture()
        {
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
            AddressRepository = new Mock<IAddressRepository>();
            DoxatagRepository = new Mock<IDoxatagRepository>();
            AddressService = new Mock<IAddressService>();
            DoxatagService = new Mock<IDoxatagService>();
            RoleService = new Mock<IRoleService>();
            SignInService = new Mock<ISignInService>();
            UserService = new Mock<IUserService>();
            EventService = new Mock<IEventService>();
            InteractionService = new Mock<IIdentityServerInteractionService>();
        }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<IAddressRepository> AddressRepository { get; }

        public Mock<IDoxatagRepository> DoxatagRepository { get; }

        public Mock<IAddressService> AddressService { get; }

        public Mock<IDoxatagService> DoxatagService { get; }

        public Mock<IRoleService> RoleService { get; }

        public Mock<ISignInService> SignInService { get; }

        public Mock<IUserService> UserService { get; }

        public Mock<IEventService> EventService { get; }

        public Mock<IIdentityServerInteractionService> InteractionService { get; }
    }
}
