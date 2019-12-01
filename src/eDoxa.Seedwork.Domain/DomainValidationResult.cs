// Filename: DomainValidationResult.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Seedwork.Domain
{
    public sealed class DomainValidationResult
    {
        public static readonly DomainValidationResult Succeded = new DomainValidationResult();

        private readonly HashSet<DomainValidationError> _errors = new HashSet<DomainValidationError>();
        private readonly DomainValidationMetadata _metadata = new DomainValidationMetadata();

        public bool IsValid => !_errors.Any();

        public IReadOnlyCollection<DomainValidationError> Errors => _errors;

        public IReadOnlyDictionary<string, object> Metadata => _metadata;

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
    }
}
