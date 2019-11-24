// Filename: FileStorage.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Infrastructure.Extensions;

namespace eDoxa.Payment.Api.Infrastructure.Data.Storage
{
    public sealed class FileStorage
    {
        private static Lazy<IImmutableSet<StripeReference>> LazyStripeReferences =>
            new Lazy<IImmutableSet<StripeReference>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/users.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(Guid)
                            })
                        .Select(record => new StripeReference(UserId.FromGuid(record.Id), "customerId", "accountId"))
                        .ToImmutableHashSet();
                });

        public static IImmutableSet<StripeReference> StripeReferences => LazyStripeReferences.Value;

        public IImmutableSet<StripeReference> GetStripeReferences()
        {
            return StripeReferences;
        }
    }
}
