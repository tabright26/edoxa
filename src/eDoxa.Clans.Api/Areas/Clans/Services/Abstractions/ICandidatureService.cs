﻿// Filename: IAccountService.cs
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
using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.Results;

namespace eDoxa.Clans.Api.Areas.Clans.Services.Abstractions
{
    public interface ICandidatureService
    {
        Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(ClanId clanId);

        Task<IReadOnlyCollection<Candidature>> FetchCandidaturesAsync(UserId userId);

        Task<Candidature?> FindCandidatureAsync(CandidatureId candidatureId);

        Task<ValidationResult> SendCandidatureAsync(UserId candidateId, ClanId clanId);

        Task<ValidationResult> AcceptCandidatureAsync(Candidature candidature, UserId ownerId);

        Task<ValidationResult> DeclineCandidatureAsync(Candidature candidature, UserId ownerId);

        Task DeleteCandidaturesAsync(UserId clanId);

        Task DeleteCandidaturesAsync(ClanId clanId);
    }
}
