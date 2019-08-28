// Filename: ScoringResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses
{
    [JsonDictionary]
    public class ScoringResponse : Dictionary<string, float>
    {
    }
}
