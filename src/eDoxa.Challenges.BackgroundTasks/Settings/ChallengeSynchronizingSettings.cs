// Filename: ChallengeSyncingSettings.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using JetBrains.Annotations;
using Newtonsoft.Json;

namespace eDoxa.Challenges.BackgroundTasks.Settings
{
    public class ChallengeSynchronizingSettings
    {
        public bool Enabled { get; set; }

        public int Delay { get; set; }

        [NotNull]
        public override string ToString()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            return $"\"{nameof(ChallengeSynchronizingSettings)}\": {json}";
        }
    }
}