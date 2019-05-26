// Filename: DomainValidator.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.Specifications;

namespace eDoxa.Seedwork.Application.Validators
{
    public abstract class DomainValidator<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        private readonly ICollection<DomainRule<TEntity>> _rules = new HashSet<DomainRule<TEntity>>();

        public bool Validate(TEntity entity, out ValidationResult result)
        {
            result = new ValidationResult();

            foreach (var rule in _rules)
            {
                if (rule.Specification.IsSatisfiedBy(entity))
                {
                    result.AddError(rule.ErrorMessage);

                    if (rule.FailFast)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        protected void AddRule(ISpecification<TEntity> specification, string errorMessage, bool failFast = false)
        {
            _rules.Add(new DomainRule<TEntity>(specification, errorMessage, failFast));
        }
    }
}
