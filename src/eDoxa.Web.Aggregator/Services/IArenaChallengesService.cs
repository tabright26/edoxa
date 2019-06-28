// Filename: IArenaChallengesService.cs
// Date Created: 2019-06-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace eDoxa.Web.Aggregator.Services
{
    public interface IArenaChallengesService
    {
        // TODO: Replace the dynamic type resolution with DTO objects.
        Task<IEnumerable<dynamic>> FetchChallenges();
    }
}
