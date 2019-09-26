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
    public sealed class CandidatureService : ICandidatureService
    {
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public CandidatureService(ICandidatureRepository candidatureRepository, IServiceBusPublisher serviceBusPublisher)
        {
            _candidatureRepository = candidatureRepository;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(UserId userId)
        {
            throw new NotImplementedException();
        }

        public Task<Candidature?> FindCandidatureAsync(CandidatureId candidatureId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> SendCandidatureAsync(ClanId clanId, UserId userId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> AcceptCandidatureAsync(Candidature candidature)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> DeclineCandidatureAsync(Candidature candidature)
        {
            throw new NotImplementedException();
        }
    }
}
