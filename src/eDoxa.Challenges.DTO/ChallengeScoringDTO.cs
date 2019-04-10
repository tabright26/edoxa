// Filename: ChallengeScoringDTO.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Challenges.DTO
{
    [JsonDictionary]
    public class ChallengeScoringDTO : Dictionary<string, float>
    {
    }
}