// Filename: CredentialResponse.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Api.Areas.Credentials.Responses
{
    public sealed class CredentialResponse
    {
        public UserId UserId { get; set; }

        public Game Game { get; set; }

        public PlayerId PlayerId { get; set; }
    }
}
