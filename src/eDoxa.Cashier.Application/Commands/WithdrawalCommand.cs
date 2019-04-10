// Filename: WithdrawalCommand.cs
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
    public class WithdrawalCommand : Command<decimal>
    {
        public WithdrawalCommand(decimal amount)
        {
            Amount = amount;
        }

        [IgnoreDataMember]
        public UserId UserId { get; set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}