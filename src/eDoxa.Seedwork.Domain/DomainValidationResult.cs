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
        public static readonly DomainValidationResult Succeded = new DomainValidationResult();

        private readonly HashSet<DomainValidationError> _errors = new HashSet<DomainValidationError>();
        private readonly DomainValidationMetadata _metadata = new DomainValidationMetadata();

        public bool IsValid => !_errors.Any();

        public IReadOnlyCollection<DomainValidationError> Errors => _errors;

        public object GetMetadataResponse()
        {
            if (IsValid)
            {
                return _metadata[DomainValidationMetadata.Response] ??
                       throw new NullReferenceException("The response metadata has not been added to the validation result.");
            }

            throw new InvalidOperationException("The validation result must be valid to return the response from the metadata.");
        }

        public static DomainValidationResult Failure(string propertyName, string errorMessage)
        {
            var result = new DomainValidationResult();

            result.AddDomainValidationError(propertyName, errorMessage);

            return result;
        }

        public static DomainValidationResult Failure(string errorMessage)
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

        public void AddMetadataResponse<TResponse>(TResponse response)
        where TResponse : class
        {
            _metadata.AddResponse(response);
        }
    }
}
