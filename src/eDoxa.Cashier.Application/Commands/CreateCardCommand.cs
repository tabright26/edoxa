// Filename: CreateCardCommand.cs
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

using Stripe;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public class CreateCardCommand : Command<Card>
    {
        public CreateCardCommand(string sourceToken, bool defaultCard = false)
        {
            SourceToken = sourceToken;
            DefaultCard = defaultCard;
        }

        [IgnoreDataMember]
        public UserId UserId { get; set; }

        [DataMember]
        public string SourceToken { get; private set; }

        [DataMember(IsRequired = false)]
        public bool DefaultCard { get; private set; }
    }
}