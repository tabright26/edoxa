// Filename: TransactionModelExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class TransactionModelExtensions
    {
        public static ITransaction ToEntity(this TransactionModel model)
        {
            var transaction = new Transaction(
                CurrencyType.FromValue(model.Currency).ToCurrency(model.Amount),
                new TransactionDescription(model.Description),
                TransactionType.FromValue(model.Type),
                new DateTimeProvider(model.Timestamp),
                new TransactionMetadata(model.Metadata.ToDictionary(metadata => metadata.Key, metadata => metadata.Value)));

            transaction.SetEntityId(model.Id);

            var status = TransactionStatus.FromValue(model.Status);

            if (status == TransactionStatus.Succeeded)
            {
                transaction.MarkAsSucceeded();
            }

            if (status == TransactionStatus.Failed)
            {
                transaction.MarkAsFailed();
            }

            transaction.ClearDomainEvents();

            return transaction;
        }
    }
}
