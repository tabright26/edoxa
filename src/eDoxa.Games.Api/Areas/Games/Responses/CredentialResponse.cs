﻿// Filename: CredentialResponse.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Api.Areas.Games.Responses
{
    public sealed class CredentialResponse
    {
        public UserId UserId { get; set; }

        public Game Game { get; set; }

        public PlayerId PlayerId { get; set; }
    }
}
