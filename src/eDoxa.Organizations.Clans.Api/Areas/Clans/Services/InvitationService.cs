// Filename: AccountService.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.ServiceBus.Abstractions;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Services
{
    public sealed class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IClanService _clanService;

        public InvitationService(IInvitationRepository invitationRepository, IServiceBusPublisher serviceBusPublisher, IClanService clanService)
        {
            _invitationRepository = invitationRepository;
            _serviceBusPublisher = serviceBusPublisher;
            _clanService = clanService;
        }

        public async Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(ClanId clanId)
        {
            var invitations = await _invitationRepository.FetchAsync();
            return invitations.Where(invitation => invitation.ClanId == clanId).ToList();
        }

        public async Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(UserId userId)
        {
            var invitations = await _invitationRepository.FetchAsync();
            return invitations.Where(invitation => invitation.UserId == userId).ToList();
        }

        public async Task<Invitation?> FindInvitationAsync(InvitationId invitationId)
        {
            return await _invitationRepository.FindAsync(invitationId);
        }

        public async Task<ValidationResult> SendInvitationAsync(ClanId clanId, UserId userId)
        {
            var invitations = await _invitationRepository.FetchAsync();

            if (invitations.Any(clan => clan.UserId == userId && clan.ClanId == clanId))
            {
                var failure = new ValidationFailure(string.Empty, "The invitation from this clan to this member already exist.");
                return failure.ToResult();
            }
            _invitationRepository.Create(new Invitation(userId, clanId));
            await _invitationRepository.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> AcceptInvitationAsync(Invitation invitation)
        {
            //Todo check if ok
            var clan = await _clanService.FindClanAsync(invitation.ClanId);

            if (clan == null)
            {
                var failure = new ValidationFailure(string.Empty, "Clan does not exist.");
                return failure.ToResult();
            }

            await _clanService.AddMemberToClanAsync(clan, invitation);
            _invitationRepository.Delete(invitation);
            await _invitationRepository.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> DeclineInvitationAsync(Invitation invitation)
        {
            _invitationRepository.Delete(invitation);
            await _invitationRepository.CommitAsync();
            return new ValidationResult();
        }

    }
}
