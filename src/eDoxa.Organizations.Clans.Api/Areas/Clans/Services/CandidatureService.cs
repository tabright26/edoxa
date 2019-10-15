// Filename: CandidatureService.cs
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
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Services
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

        public async Task<ValidationResult> SendCandidatureAsync(UserId userId, ClanId clanId)
        {
            if (await _clanRepository.IsMemberAsync(userId))
            {
                return new ValidationFailure(string.Empty, "User already in a clan.").ToResult();
            }

            if (await _candidatureRepository.ExistsAsync(userId, clanId))
            {
                return new ValidationFailure("_error", "The candidature of this member for that clan already exist.").ToResult();
            }

            var candidature = new Candidature(userId, clanId);

            _candidatureRepository.Create(candidature);

            await _candidatureRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> AcceptCandidatureAsync(Candidature candidature, UserId ownerId)
        {
            if (!await _clanRepository.IsOwnerAsync(candidature.ClanId, ownerId))
            {
                return new ValidationFailure(string.Empty, "Permission required.").ToResult();
            }

            candidature.Accept();

            await _candidatureRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> DeclineCandidatureAsync(Candidature candidature, UserId userId)
        {
            if (!await _clanRepository.IsOwnerAsync(candidature.ClanId, userId))
            {
                return new ValidationFailure(string.Empty, "Permission required.").ToResult();
            }

            _candidatureRepository.Delete(candidature);

            await _candidatureRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
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
