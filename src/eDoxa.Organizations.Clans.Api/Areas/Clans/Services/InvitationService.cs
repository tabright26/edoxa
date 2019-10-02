// Filename: InvitationService.cs
// Date Created: 2019-09-30
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Services
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

        public async Task<ValidationResult> SendInvitationAsync(ClanId clanId, UserId userId, UserId ownerId)
        {
            if (!await _clanRepository.IsOwnerAsync(clanId, ownerId))
            {
                return new ValidationFailure(string.Empty, "Permission required.").ToResult();
            }

            if (await _clanRepository.IsMemberAsync(userId))
            {
                return new ValidationFailure(string.Empty, "Target already in a clan.").ToResult();
            }

            if (await _invitationRepository.ExistsAsync(ownerId, clanId))
            {
                return new ValidationFailure(string.Empty, "The invitation from this clan to that member already exist.").ToResult();
            }

            var invitation = new Invitation(userId, clanId);

            _invitationRepository.Create(invitation);

            await _invitationRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> AcceptInvitationAsync(Invitation invitation, UserId userId)
        {
            if (invitation.UserId != userId)
            {
                return new ValidationFailure(string.Empty, $"The user {userId} can not accept someone else invitation.").ToResult();
            }

            invitation.Accept();

            return new ValidationResult();
        }

        public async Task<ValidationResult> DeclineInvitationAsync(Invitation invitation, UserId userId)
        {
            if (invitation.UserId != userId)
            {
                return new ValidationFailure(string.Empty, $"The user {userId} can not decline someone else invitation.").ToResult();
            }

            _invitationRepository.Delete(invitation);

            await _invitationRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
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
