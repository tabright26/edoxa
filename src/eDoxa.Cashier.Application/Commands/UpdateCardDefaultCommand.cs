// Filename: UpdateDefaultCardCommand.cs
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
using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class UpdateCardDefaultCommand : Command<IActionResult>
    {
        public UpdateCardDefaultCommand(CardId cardId)
        {
            CardId = cardId;
        }
        
        [IgnoreDataMember] public CardId CardId { get; private set; }
    }
}