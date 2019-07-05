// Filename: TransactionConverter.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class TransactionConverter : ITypeConverter<TransactionModel, ITransaction>
    {
        [NotNull]
        public ITransaction Convert([NotNull] TransactionModel source, [NotNull] ITransaction destination, [NotNull] ResolutionContext context)
        {
            var transaction = new Transaction(
                Convert(source.Amount, Currency.FromValue(source.Currency)),
                new TransactionDescription(source.Description),
                TransactionType.FromValue(source.Type),
                new TimestampDateTimeProvider(source.Timestamp)
            );

            transaction.SetEntityId(TransactionId.FromGuid(source.Id));

            var status = TransactionStatus.FromValue(source.Status);

            if (status == TransactionStatus.Succeded)
            {
                transaction.MarkAsSucceded();
            }

            if (status == TransactionStatus.Failed)
            {
                transaction.MarkAsFailed();
            }

            return transaction;
        }

        private static ICurrency Convert(decimal amount, Currency currency)
        {
            if (currency == Currency.Money)
            {
                return new Money(amount);
            }

            return new Token(amount);
        }

        private sealed class TimestampDateTimeProvider : IDateTimeProvider
        {
            private DateTime _timestamp;

            public TimestampDateTimeProvider(DateTime timestamp)
            {
                _timestamp = timestamp;
            }

            public DateTime DateTime => _timestamp;
        }
    }
}
