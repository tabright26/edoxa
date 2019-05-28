// Filename: WithdrawMoneyCommand.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Commands.Abstractions;
using eDoxa.Functional;

using FluentValidation.Results;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class WithdrawMoneyCommand : Command<Either<ValidationResult, TransactionStatus>>
    {
        public WithdrawMoneyCommand(MoneyWithdrawBundleType bundleType)
        {
            BundleType = bundleType;
        }

        [DataMember(Name = "bundleType")]
        public MoneyWithdrawBundleType BundleType { get; private set; }
    }
}
