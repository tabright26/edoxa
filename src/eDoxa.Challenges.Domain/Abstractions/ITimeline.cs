// Filename: IChallengeTimeline.cs
// Date Created: 2019-04-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.Abstractions
{
    public interface ITimeline
    {
        TimeSpan? RegistrationPeriod { get; }

        TimeSpan? ExtensionPeriod { get; }
    }
}