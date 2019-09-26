// Filename: AccountService.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Services
{
    public sealed class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public InvitationService(IInvitationRepository invitationRepository, IServiceBusPublisher serviceBusPublisher)
        {
            _invitationRepository = invitationRepository;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(UserId userId)
        {
            throw new NotImplementedException();
        }

        public Task<Invitation?> FindInvitationAsync(InvitationId invitationId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> SendInvitationAsync(ClanId clanId, UserId userId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> AcceptInvitationAsync(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> DeclineInvitationAsync(Invitation invitation)
        {
            throw new NotImplementedException();
        }
    }
}
