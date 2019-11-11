// Filename: ScoringResponse.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Responses
{
    [JsonDictionary]
    public class ScoringResponse : Dictionary<string, float>
    {
    }
}
