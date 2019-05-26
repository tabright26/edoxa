// Filename: CreateTokenChallengeCommand.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Application.Commands.Abstractions;
using eDoxa.Arena.Challenges.Domain;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Application.Commands
{
    public sealed class CreateTokenChallengeCommand : CreateChallengeCommand<TokenEntryFee>
    {
        public CreateTokenChallengeCommand(
            string name,
            Game game,
            BestOf bestOf,
            PayoutEntries payoutEntries,
            TokenEntryFee entryFee,
            bool isFakeChallenge = false
        ) : base(
            name,
            game,
            bestOf,
            payoutEntries,
            entryFee,
            isFakeChallenge
        )
        {
        }
    }
}
