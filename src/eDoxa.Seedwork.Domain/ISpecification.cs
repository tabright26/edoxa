// Filename: ISpecification.cs
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
    ///     Specification pattern allows encapsulate some piece of domain knowledge into a single unit.
    /// </summary>
    /// <remarks>
    ///     This pattern is used to ensure the DRY principle.
    /// </remarks>
    /// <typeparam name="TEntity">An entity of the domain.</typeparam>
    public interface ISpecification<in TEntity>
    where TEntity : IAggregateRoot
    {
        /// <summary>
        ///     Evaluate specification satisfaction for an entity.
        /// </summary>
        /// <param name="entity">An entity of the domain.</param>
        /// <returns><c>true</c>, if the specification is satisfied by the entity, otherwise <c>false</c>.</returns>
        bool IsSatisfiedBy(TEntity entity);
    }
}