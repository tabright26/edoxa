// Filename: GameViewModel.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Identity.Api.Areas.Identity.ViewModels
{
    public class GameViewModel
    {
        public string Name { get; set; }

        public bool IsLinked { get; set; }

        public bool IsSupported { get; set; }
    }
}
