// Filename: TransactionTypeConverter.cs
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
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class TransactionTypeConverter : ITypeConverter<TransactionModel, ITransaction>
    {
        [NotNull]
        public ITransaction Convert([NotNull] TransactionModel source, [NotNull] ITransaction destination, [NotNull] ResolutionContext context)
        {
            var transaction = new Transaction(
                Convert(source.Amount, CurrencyType.FromValue(source.Currency)),
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

        private static Currency Convert(decimal amount, CurrencyType currencyType)
        {
            if (currencyType == CurrencyType.Money)
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
