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

using eDoxa.Organizations.Clans.Domain.Models;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Domain.Services
{
    public interface ICandidatureService
    {
        Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(ClanId clanId);

        Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(UserId userId);

        Task<Candidature?> FindCandidatureAsync(CandidatureId candidatureId);

        Task<ValidationResult> SendCandidatureAsync(ClanId clanId, UserId userId);

        Task<ValidationResult> AcceptCandidatureAsync(Candidature candidature);

        Task<ValidationResult> DeclineCandidatureAsync(Candidature candidature);
    }
}
