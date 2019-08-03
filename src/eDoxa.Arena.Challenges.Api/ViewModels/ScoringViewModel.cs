// Filename: ScoringViewModel.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonDictionary]
    public class ScoringViewModel : Dictionary<string, float>
    {
    }
}
