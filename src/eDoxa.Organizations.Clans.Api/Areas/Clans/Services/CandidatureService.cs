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
    public sealed class CandidatureService : ICandidatureService
    {
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IClanService _clanService;

        public CandidatureService(ICandidatureRepository candidatureRepository, IServiceBusPublisher serviceBusPublisher, IClanService clanService)
        {
            _candidatureRepository = candidatureRepository;
            _serviceBusPublisher = serviceBusPublisher;
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

        public async Task<ValidationResult> SendCandidatureAsync(ClanId clanId, UserId userId)
        {
            var candidatures = await _candidatureRepository.FetchAsync();

            if (candidatures.Any(clan => clan.UserId == userId && clan.ClanId == clanId))
            {
                var failure = new ValidationFailure(string.Empty, "The candidature of this member for that clan already exist.");
                return failure.ToResult();
            }
            _candidatureRepository.Create(new Candidature(userId, clanId));
            await _candidatureRepository.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> AcceptCandidatureAsync(Candidature candidature)
        {
            //Todo check if ok
            var clan = await _clanService.FindClanAsync(candidature.ClanId);

            if (clan == null)
            {
                var failure = new ValidationFailure(string.Empty, "Clan does not exist.");
                return failure.ToResult();
            }

            await _clanService.AddMemberToClanAsync(clan, candidature);
            _candidatureRepository.Delete(candidature);
            await _candidatureRepository.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> DeclineCandidatureAsync(Candidature candidature)
        {
            _candidatureRepository.Delete(candidature);
            await _candidatureRepository.CommitAsync();
            return new ValidationResult();
        }
    }
}
