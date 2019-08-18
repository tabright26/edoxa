// Filename: IRepository.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Domain
{
    public interface IRepository<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
