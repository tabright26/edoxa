// Filename: DomainRule.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;
using eDoxa.Specifications;

namespace eDoxa.Seedwork.Application.Validators
{
    public sealed class DomainRule<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        public DomainRule(ISpecification<TEntity> specification, string errorMessage, bool failFast = false)
        {
            Specification = specification;
            ErrorMessage = errorMessage;
            FailFast = failFast;
        }

        public ISpecification<TEntity> Specification { get; }

        public string ErrorMessage { get; }

        public bool FailFast { get; }
    }
}
