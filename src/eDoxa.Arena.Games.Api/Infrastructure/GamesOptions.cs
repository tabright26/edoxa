// Filename: GamesOptions.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

namespace eDoxa.Arena.Games.Api.Infrastructure
{
    public sealed class GamesOptions
    {
        public IDictionary<string, GameOptions> Games { get; set; }
    }

    public sealed class GameOptions : ServiceOptions
    {
        public ServiceOptions Challenge { get; set; }

        public ServiceOptions Tournament { get; set; }
    }

    public class ServiceOptions
    {
        public bool Enabled { get; set; }

        public bool Displayed { get; set; }
    }
}
