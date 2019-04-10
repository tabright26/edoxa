// Filename: DeleteCardCommand.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Commands;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public class DeleteCardCommand : Command
    {
        public DeleteCardCommand(UserId userId, CardId cardId)
        {
            UserId = userId;
            CardId = cardId;
        }

        [IgnoreDataMember]
        public UserId UserId { get; private set; }

        [IgnoreDataMember]
        public CardId CardId { get; private set; }
    }
}