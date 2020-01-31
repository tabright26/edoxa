// Filename: TransactionExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class TransactionExtensions
    {
        public static TransactionModel ToModel(this ITransaction transaction)
        {
            return new TransactionModel
            {
                Id = transaction.Id,
                Timestamp = transaction.Timestamp,
                Description = transaction.Description.Text,
                Amount = transaction.Currency.Amount,
                Currency = transaction.Currency.Type.Value,
                Type = transaction.Type.Value,
                Status = transaction.Status.Value,
                Metadata = transaction.Metadata.Select(
                        metadata => new TransactionMetadataModel
                        {
                            Key = metadata.Key,
                            Value = metadata.Value
                        })
                    .ToList(),
                DomainEvents = transaction.DomainEvents.ToList()
            };
        }
    }
}
