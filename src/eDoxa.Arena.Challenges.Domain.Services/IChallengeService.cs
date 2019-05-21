﻿// Filename: IChallengeService.cs
// Date Created: 2019-05-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public interface IChallengeService
    {
        Task CompleteAsync(CancellationToken cancellationToken = default);

        Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default);
    }
}