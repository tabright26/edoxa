// Filename: CandidatureService.cs
// Date Created: 2019-09-25
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
    public sealed class CandidatureService : ICandidatureService
    {
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IClanService _clanService;

        public CandidatureService(ICandidatureRepository candidatureRepository, IInvitationRepository invitationRepository, IClanService clanService)
        {
            _candidatureRepository = candidatureRepository;
            _invitationRepository = invitationRepository;
            _clanService = clanService;
        }

        public async Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(ClanId clanId)
        {
            var candidatures = await _candidatureRepository.FetchAsync();

            return candidatures.Where(candidature => candidature.ClanId == clanId).ToList();
        }

        public async Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(UserId userId)
        {
            var candidatures = await _candidatureRepository.FetchAsync();

            return candidatures.Where(candidature => candidature.UserId == userId).ToList();
        }

        public async Task<Candidature?> FindCandidatureAsync(CandidatureId candidatureId)
        {
            return await _candidatureRepository.FindAsync(candidatureId);
        }

        public async Task<ValidationResult> SendCandidatureAsync(UserId userId, ClanId clanId)
        {
            if ( await _candidatureRepository.ExistsAsync(userId, clanId))
            {
                var failure = new ValidationFailure(string.Empty, "The candidature of this member for that clan already exist.");

                return failure.ToResult();
            }

            if (await _clanService.HasMember(userId))
            {
                var failure = new ValidationFailure(string.Empty, "User already in a clan.");

                return failure.ToResult();
            }

            _candidatureRepository.Create(new Candidature(userId, clanId));
            await _candidatureRepository.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> AcceptCandidatureAsync(UserId recruiterId, Candidature candidature)
        {
            var clan = await _clanService.FindClanAsync(candidature.ClanId);

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

            clan.AddMember(candidature);

            await _candidatureRepository.DeleteAllWith(candidature.UserId);
            await _invitationRepository.DeleteAllWith(candidature.UserId);

            await _candidatureRepository.CommitAsync();
            await _invitationRepository.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> DeclineCandidatureAsync(Candidature candidature)
        {
            _candidatureRepository.Delete(candidature);
            await _candidatureRepository.CommitAsync();

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
