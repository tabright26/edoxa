// Filename: IRepository.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.Domain
{
    /// <summary>
    ///     Base interface for implement a "Repository Pattern".
    /// </summary>
    /// <typeparam name="TAggregateRoot">Type of aggregate root entity for this repository.</typeparam>
    public interface IRepository<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot
    {
        /// <summary>
        ///     Unit of work access for repository.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}