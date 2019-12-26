// Filename: DomainValidationResult.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Seedwork.Domain
{
    public sealed class DomainValidationResult : IDomainValidationResult
    {
        private readonly HashSet<DomainValidationError> _errors = new HashSet<DomainValidationError>();
        private readonly DomainValidationMetadata _metadata = new DomainValidationMetadata();

        public bool IsValid => !_errors.Any();

        public IReadOnlyCollection<DomainValidationError> Errors => _errors;

        public TEntity GetEntityFromMetadata<TEntity>()
        where TEntity : class
        {
            if (IsValid)
            {
                return _metadata.GetEntity<TEntity>();
            }

            throw new InvalidOperationException("The validation result must be valid to return the response from the metadata.");
        }

        public void AddEntityToMetadata<TEntity>(TEntity entity)
        where TEntity : class
        {
            _metadata.AddEntity(entity);
        }

        public static IDomainValidationResult Succeded<TEntity>(TEntity entity)
        where TEntity : class
        {
            var result = new DomainValidationResult();

            result.AddEntityToMetadata(entity);

            return result;
        }

        public static IDomainValidationResult Failure(string propertyName, string errorMessage)
        {
            var result = new DomainValidationResult();

            result.AddDomainValidationError(propertyName, errorMessage);

            return result;
        }

        public static IDomainValidationResult Failure(string errorMessage)
        {
            var result = new DomainValidationResult();

            result.AddDomainValidationError(errorMessage);

            return result;
        }

        public void AddDomainValidationError(string propertyName, string errorMessage)
        {
            _errors.Add(new DomainValidationError(propertyName, errorMessage));
        }

        public void AddDomainValidationError(string errorMessage)
        {
            _errors.Add(new DomainValidationError(errorMessage));
        }
    }
}
