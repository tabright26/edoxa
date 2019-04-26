// Filename: AddFundsCommand.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Application.Commands;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public class DepositMoneyCommand : Command<decimal>
    {
        public DepositMoneyCommand(MoneyBundleType bundleType)
        {
            BundleType = bundleType;
        }

        [IgnoreDataMember]
        public UserId UserId { get; set; }

        [DataMember(Name = "packType")]
        public MoneyBundleType BundleType { get; private set; }
    }
}