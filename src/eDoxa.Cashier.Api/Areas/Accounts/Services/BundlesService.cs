// Filename: BundlesService.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Services;

using Microsoft.Extensions.Options;

namespace eDoxa.Cashier.Api.Areas.Accounts.Services
{
    public sealed class BundlesService : IBundlesService
    {
        public BundlesService(IOptions<BundlesOptions> options)
        {
            Options = options.Value;
        }

        private BundlesOptions Options { get; }

        public IImmutableSet<Bundle> FetchDepositMoneyBundles()
        {
            return Options.Deposit[Currency.Money.Name]
                .Select(bundle => new Bundle(new Money(bundle.Amount), new Price(new Money(bundle.Price))))
                .ToImmutableHashSet();
        }

        public IImmutableSet<Bundle> FetchDepositTokenBundles()
        {
            return Options.Deposit[Currency.Token.Name]
                .Select(bundle => new Bundle(new Token(bundle.Amount), new Price(new Money(bundle.Price))))
                .ToImmutableHashSet();
        }

        public IImmutableSet<Bundle> FetchWithdrawalMoneyBundles()
        {
            return Options.Withdrawal[Currency.Money.Name]
                .Select(bundle => new Bundle(new Money(bundle.Amount), new Price(new Money(bundle.Price))))
                .ToImmutableHashSet();
        }
    }
}
