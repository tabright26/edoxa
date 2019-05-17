// Filename: WithdrawalMoneyCommand.cs
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

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class WithdrawMoneyCommand : Command<Either<TransactionStatus>>
    {
        public WithdrawMoneyCommand(MoneyWithdrawalBundleType bundleType)
        {
            BundleType = bundleType;
        }

        [DataMember(Name = "bundleType")] public MoneyWithdrawalBundleType BundleType { get; private set; }
    }
}