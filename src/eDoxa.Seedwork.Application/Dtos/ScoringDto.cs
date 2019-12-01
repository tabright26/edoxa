// Filename: ScoringDto.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Dtos
{
    [JsonDictionary]
    public sealed class ScoringDto : Dictionary<string, float>
    {
        [JsonConstructor]
        public ScoringDto(IDictionary<string, float> scoring) : base(scoring)
        {
        }
    }
}
