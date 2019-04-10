﻿// Filename: IChallengeSynchronizerService.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.Challenges.Domain.Services
{
    public interface IChallengeSynchronizerService
    {
        Task SynchronizeAsync();
    }
}