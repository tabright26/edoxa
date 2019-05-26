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
        private readonly IDictionary<string, ISpecification<TEntity>> _specifications = new Dictionary<string, ISpecification<TEntity>>();

        public bool Validate(TEntity entity, out ValidationResult result)
        {
            result = new ValidationResult();

            foreach (var specification in _specifications)
            {
                if (specification.Value.IsSatisfiedBy(entity))
                {
                    result.AddError(specification.Key);
                }
            }

            return result;
        }

        protected void AddRule(ISpecification<TEntity> specification, string errorMessage)
        {
            _specifications.Add(errorMessage, specification);
        }
    }
}
