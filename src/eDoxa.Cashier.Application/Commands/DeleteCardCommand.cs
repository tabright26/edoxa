// Filename: DeleteCardCommand.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Commands;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class DeleteCardCommand : Command<IActionResult>
    {
        public DeleteCardCommand(UserId userId, CardId cardId)
        {
            UserId = userId;
            CardId = cardId;
        }

        [IgnoreDataMember] public UserId UserId { get; private set; }

        [IgnoreDataMember] public CardId CardId { get; private set; }
    }
}