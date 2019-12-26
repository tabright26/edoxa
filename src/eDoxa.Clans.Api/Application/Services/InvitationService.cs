// Filename: InvitationService.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Api.Application.Services
{
    public sealed class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IClanRepository _clanRepository;

        public InvitationService(IInvitationRepository invitationRepository, IClanRepository clanRepository)
        {
            _invitationRepository = invitationRepository;
            _clanRepository = clanRepository;
        }

        public async Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(ClanId clanId)
        {
            return await _invitationRepository.FetchAsync(clanId);
        }

        public async Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(UserId userId)
        {
            return await _invitationRepository.FetchAsync(userId);
        }

        public async Task<Invitation?> FindInvitationAsync(InvitationId invitationId)
        {
            return await _invitationRepository.FindAsync(invitationId);
        }

        public async Task<IDomainValidationResult> SendInvitationAsync(ClanId clanId, UserId userId, UserId ownerId)
        {
            if (!await _clanRepository.IsOwnerAsync(clanId, ownerId))
            {
                return DomainValidationResult.Failure("Permission required.");
            }

            if (await _clanRepository.IsMemberAsync(userId))
            {
                return DomainValidationResult.Failure("Target already in a clan.");
            }

            if (await _invitationRepository.ExistsAsync(ownerId, clanId))
            {
                return DomainValidationResult.Failure("_error", "The invitation from this clan to that member already exist.");
            }

            var invitation = new Invitation(userId, clanId);

            _invitationRepository.Create(invitation);

            await _invitationRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult();
        }

        public async Task<IDomainValidationResult> AcceptInvitationAsync(Invitation invitation, UserId userId)
        {
            if (invitation.UserId != userId)
            {
                return DomainValidationResult.Failure($"The user {userId} can not accept someone else invitation.");
            }

            invitation.Accept();

            await _invitationRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult();
        }

        public async Task<IDomainValidationResult> DeclineInvitationAsync(Invitation invitation, UserId userId)
        {
            if (invitation.UserId != userId)
            {
                return DomainValidationResult.Failure($"The user {userId} can not decline someone else invitation.");
            }

            _invitationRepository.Delete(invitation);

            await _invitationRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult();
        }

        public async Task DeleteInvitationsAsync(ClanId clanId)
        {
            foreach (var invitation in await this.FetchInvitationsAsync(clanId))
            {
                _invitationRepository.Delete(invitation);
            }

            await _invitationRepository.UnitOfWork.CommitAsync();
        }

        public async Task DeleteInvitationsAsync(UserId userId)
        {
            foreach (var invitation in await this.FetchInvitationsAsync(userId))
            {
                _invitationRepository.Delete(invitation);
            }

            await _invitationRepository.UnitOfWork.CommitAsync();
        }
    }
}
