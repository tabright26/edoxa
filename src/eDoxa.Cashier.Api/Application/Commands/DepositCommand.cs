// Filename: DepositCommand.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Seedwork.Application.Commands.Abstractions;

namespace eDoxa.Cashier.Api.Application.Commands
{
    [DataContract]
    public sealed class DepositCommand : Command
    {
        public DepositCommand(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        [DataMember(Name = "currency")]
        public string Currency { get; private set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
