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

using eDoxa.Cashier.Application.ViewModels;
using eDoxa.Seedwork.Application.Commands.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class DepositCommand : Command<TransactionViewModel>
    {
        public DepositCommand(decimal amount, CurrencyType type)
        {
            Currency = new CurrencyViewModel
            {
                Amount = amount,
                Type = type
            };
        }

        [DataMember(Name = "currency")]
        public CurrencyViewModel Currency { get; private set; }
    }
}
