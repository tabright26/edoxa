// Filename: DepositTokenValidator.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.Specifications;
using eDoxa.Seedwork.Application.Validators;

namespace eDoxa.Cashier.Domain.Services.Validators
{
    public class DepositTokenValidator : DomainValidator<TokenAccount>
    {
        public DepositTokenValidator(DateTime? lastDeposit)
        {
            this.AddRule(new DailyTokenDepositUnavailableSpecification(), $"Deposit unavailable until {lastDeposit?.AddDays(1)}");
        }
    }
}
