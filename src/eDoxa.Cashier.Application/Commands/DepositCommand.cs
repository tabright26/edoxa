// Filename: DepositCommand.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.DTO;
using eDoxa.Commands.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class DepositCommand : Command<TransactionDTO>
    {
        public DepositCommand(decimal amount, CurrencyType type)
        {
            Currency = new CurrencyDTO
            {
                Amount = amount,
                Type = type
            };
        }

        [DataMember(Name = "currency")]
        public CurrencyDTO Currency { get; private set; }
    }
}
