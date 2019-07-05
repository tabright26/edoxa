﻿// Filename: SynchronizeChallengesValidator.cs
// Date Created: 2019-06-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentValidation;

namespace eDoxa.Arena.Challenges.Domain.Validators
{
    public sealed class SynchronizeChallengesValidator : AbstractValidator<Challenge>
    {
    }
}