﻿// Filename: ChallengePayoutDTO.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace eDoxa.Challenges.DTO
{
    [JsonArray]
    public class ChallengePayoutDTO : Collection<BucketDTO>
    {
    }
}