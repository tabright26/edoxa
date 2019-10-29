// Filename: GamesOptions.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

namespace eDoxa.Arena.Games.Api.Infrastructure
{
    public sealed class GamesOptions : Dictionary<string, GameOptions>
    {
    }

    public sealed class GameOptions
    {
        public string ImageName { get; set; }

        public string ReactComponent { get; set; }

        public ServicesOptions Services { get; set; }
    }

    public sealed class ServicesOptions : Dictionary<string, ServiceOptions>
    {
    }

    public sealed class ServiceOptions
    {
        public bool Enabled { get; set; }

        public bool Displayed { get; set; }
    }
}
