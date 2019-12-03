// Filename: CandidatureService.cs
// Date Created: 2019-09-30
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

namespace eDoxa.Clans.Api.Services
{
    public sealed class CandidatureService : ICandidatureService
    {
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IClanRepository _clanRepository;

        public CandidatureService(ICandidatureRepository candidatureRepository, IClanRepository clanRepository)
        {
            _candidatureRepository = candidatureRepository;
            _clanRepository = clanRepository;
        }

        public async Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(ClanId clanId)
        {
            return await _candidatureRepository.FetchAsync(clanId);
        }

        public async Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(UserId userId)
        {
            return await _candidatureRepository.FetchAsync(userId);
        }

        public async Task<Candidature?> FindCandidatureAsync(CandidatureId candidatureId)
        {
            return await _candidatureRepository.FindAsync(candidatureId);
        }

        public async Task<DomainValidationResult> SendCandidatureAsync(UserId userId, ClanId clanId)
        {
            if (await _clanRepository.IsMemberAsync(userId))
            {
                return DomainValidationResult.Failure(string.Empty, "User already in a clan.");
            }

            if (await _candidatureRepository.ExistsAsync(userId, clanId))
            {
                return DomainValidationResult.Failure("_error", "The candidature of this member for that clan already exist.");
            }

            var candidature = new Candidature(userId, clanId);

            _candidatureRepository.Create(candidature);

            await _candidatureRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult();
        }

        public async Task<DomainValidationResult> AcceptCandidatureAsync(Candidature candidature, UserId ownerId)
        {
            if (!await _clanRepository.IsOwnerAsync(candidature.ClanId, ownerId))
            {
                return DomainValidationResult.Failure("Permission required.");
            }

            candidature.Accept();

            await _candidatureRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult();
        }

        public async Task<DomainValidationResult> DeclineCandidatureAsync(Candidature candidature, UserId userId)
        {
            if (!await _clanRepository.IsOwnerAsync(candidature.ClanId, userId))
            {
                return DomainValidationResult.Failure("Permission required.");
            }

            _candidatureRepository.Delete(candidature);

            await _candidatureRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult();
        }

        public async Task DeleteCandidaturesAsync(ClanId clanId)
        {
            foreach (var candidature in await this.FetchCandidaturesAsync(clanId))
            {
                _candidatureRepository.Delete(candidature);
            }

            await _candidatureRepository.UnitOfWork.CommitAsync();
        }

        public async Task DeleteCandidaturesAsync(UserId userId)
        {
            foreach (var candidature in await this.FetchCandidaturesAsync(userId))
            {
                _candidatureRepository.Delete(candidature);
            }

            await _candidatureRepository.UnitOfWork.CommitAsync();
        }
    }
}
