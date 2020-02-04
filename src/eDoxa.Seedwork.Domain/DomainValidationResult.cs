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
    public sealed class DomainValidationResult<TResponse>
    where TResponse : class
    {
        private readonly HashSet<DomainValidationError> _errors = new HashSet<DomainValidationError>();
        private TResponse? _response;

        public bool IsValid => !_errors.Any();

        public TResponse Response
        {
            get
            {
                if (IsValid)
                {
                    return _response ?? throw new InvalidOperationException("The validation result response isn't set.");
                }

                throw new InvalidOperationException("The validation result must be valid to return a response.");
            }
        }

        public IReadOnlyCollection<DomainValidationError> Errors => _errors;

        public static implicit operator DomainValidationResult<TResponse>(TResponse response)
        {
            return Succeeded(response);
        }

        private void AddResponse(TResponse response)
        {
            _response = response;
        }

        public DomainValidationResult<TResponse> AddInvalidArgumentError(string propertyName, string errorMessage)
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

        public DomainValidationResult<TResponse> AddFailedPreconditionError(string errorMessage)
        {
            return this.AddError(DomainValidationError.FailedPreconditionPropertyName, errorMessage);
        }

        public DomainValidationResult<TResponse> AddDebugError(string errorMessage)
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

        public static DomainValidationResult<TResponse> Succeeded(TResponse response)
        {
            var result = new DomainValidationResult<TResponse>();

            result.AddResponse(response);

            return result;
        }

        public static DomainValidationResult<TResponse> Failure(string propertyName, string errorMessage)
        {
            return new DomainValidationResult<TResponse>().AddInvalidArgumentError(propertyName, errorMessage);
        }

        public static DomainValidationResult<TResponse> Failure(string errorMessage)
        {
            return new DomainValidationResult<TResponse>().AddFailedPreconditionError(errorMessage);
        }

        private DomainValidationResult<TResponse> AddError(string propertyName, string errorMessage)
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
