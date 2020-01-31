// Filename: Repository.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace eDoxa.Seedwork.Infrastructure
{
    public abstract class Repository<T, TModel> : IRepository
    where T : class
    where TModel : class
    {
        protected readonly IDictionary<T, TModel> Mappings = new Dictionary<T, TModel>();

        public abstract Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default);
    }

    public interface IRepository
    {
        public Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default);
    }
}
