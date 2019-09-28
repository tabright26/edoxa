// Filename: AccountService.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IClanService _clanService;

        public InvitationService(IInvitationRepository invitationRepository, ICandidatureRepository candidatureRepository, IClanService clanService)
        {
            _invitationRepository = invitationRepository;
            _candidatureRepository = candidatureRepository;
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

        public async Task<ValidationResult> SendInvitationAsync(UserId recruiterId, ClanId recruiterClan, UserId inviteId)
        {
            var exists = await _invitationRepository.ExistsAsync(recruiterId, recruiterClan);

            if (exists)
            {
                var failure = new ValidationFailure(string.Empty, "The invitation from this clan to that member already exist.");
                return failure.ToResult();
            }

            if (await _clanService.HasMember(inviteId))
            {
                var failure = new ValidationFailure(string.Empty, "Target already in a clan.");

                return failure.ToResult();
            }

            var clan = await _clanService.FindClanAsync(recruiterClan);

            if (clan == null) // Make sure the specified clan still exist.
            {
                var failure = new ValidationFailure(string.Empty, "Clan does not exist.");
                return failure.ToResult();
            }

            if (!clan.IsOwner(recruiterId)) //Is the admin of said clan.
            {
                var failure = new ValidationFailure(string.Empty, "Permission required.");

                return failure.ToResult();
            }

            _invitationRepository.Create(new Invitation(inviteId, recruiterClan));
            await _invitationRepository.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> AcceptInvitationAsync(UserId userId, Invitation invitation)
        {
            var clan = await _clanService.FindClanAsync(invitation.ClanId);

            if (clan == null)
            {
                var failure = new ValidationFailure(string.Empty, "Clan does not exist.");
                return failure.ToResult();
            }

            clan.AddMember(invitation);

            await _candidatureRepository.DeleteAllWith(userId);
            await _invitationRepository.DeleteAllWith(userId);

            await _candidatureRepository.CommitAsync();
            await _invitationRepository.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> DeclineInvitationAsync(Invitation invitation)
        {
            _invitationRepository.Delete(invitation);
            await _invitationRepository.CommitAsync();
            return new ValidationResult();
        }

        //-----------------------------------------------------------------------------------------------------
        //Clan Service
        public async Task<Clan?> FindClanAsync(ClanId clanId)
        {
            return await _clanService.FindClanAsync(clanId);
        }
    }

}
