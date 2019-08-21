// Filename: ICashierFileStorage.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public interface ICashierFileStorage
    {
        Task<ILookup<PayoutEntries, PayoutLevel>> GetChallengePayoutStructuresAsync();
    }
}
