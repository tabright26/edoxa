// Filename: DomainValidationResult.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

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

        public IDomainValidationResult AddInvalidArgumentError(string propertyName, string errorMessage)
        {
            if (propertyName == DomainValidationError.FailedPreconditionPropertyName)
            {
                throw new ArgumentException($"Use the {nameof(this.AddFailedPreconditionError)} method instead.");
            }

            if (propertyName == DomainValidationError.DebugPropertyName)
            {
                throw new ArgumentException($"Use the {nameof(this.AddDebugError)} method instead.");
            }

            return this.AddError(propertyName, errorMessage);
        }

        public IDomainValidationResult AddFailedPreconditionError(string errorMessage)
        {
            return this.AddError(DomainValidationError.FailedPreconditionPropertyName, errorMessage);
        }

        public IDomainValidationResult AddDebugError(string errorMessage)
        {
            return this.AddError(DomainValidationError.DebugPropertyName, errorMessage);
        }

        public string ToJsonErrors()
        {
            return JsonConvert.SerializeObject(
                _errors.GroupBy(error => error.PropertyName)
                    .Where(grouping => grouping.Key != DomainValidationError.DebugPropertyName)
                    .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(error => error.ErrorMessage)),
                Formatting.None);
        }

        public static IDomainValidationResult Succeeded<TEntity>(TEntity entity)
        where TEntity : class
        {
            var result = new DomainValidationResult();

            result.AddEntityToMetadata(entity);

            return result;
        }

        public static IDomainValidationResult Failure(string propertyName, string errorMessage)
        {
            return new DomainValidationResult().AddInvalidArgumentError(propertyName, errorMessage);
        }

        public static IDomainValidationResult Failure(string errorMessage)
        {
            return new DomainValidationResult().AddFailedPreconditionError(errorMessage);
        }

        private IDomainValidationResult AddError(string propertyName, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException();
            }

            _errors.Add(new DomainValidationError(propertyName, errorMessage));

            return this;
        }
    }
}
