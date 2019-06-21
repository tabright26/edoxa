// Filename: ScoringViewModel.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Domain.ViewModels
{
    [JsonDictionary]
    public class ScoringViewModel : Dictionary<string, float>
    {
    }
}
