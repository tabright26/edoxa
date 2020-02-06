// Filename: IAccountService.cs
// Date Created: 2019-07-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Domain.Services
{
    public interface ICandidatureService
    {
        Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(ClanId clanId);

        Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(UserId userId);

        Task<Candidature?> FindCandidatureAsync(CandidatureId candidatureId);

        Task<DomainValidationResult<Candidature>> SendCandidatureAsync(UserId candidateId, ClanId clanId);

        Task<DomainValidationResult<Candidature>> AcceptCandidatureAsync(Candidature candidature, UserId ownerId);

        Task<DomainValidationResult<Candidature>> DeclineCandidatureAsync(Candidature candidature, UserId ownerId);

        Task DeleteCandidaturesAsync(UserId clanId);

        Task DeleteCandidaturesAsync(ClanId clanId);
    }
}
