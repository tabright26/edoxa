// Filename: ValidationResult.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Seedwork.Domain.Validations
{
    public sealed class ValidationResult
    {
        private readonly HashSet<ValidationError> _errors;

        public ValidationResult()
        {
            _errors = new HashSet<ValidationError>();
        }

        public bool Failure => _errors.Any();

        public ValidationError ValidationError => _errors.First();

        public IReadOnlyCollection<ValidationError> Errors => _errors;

        public void AddError(string message)
        {
            _errors.Add(new ValidationError(message));
        }

        public static implicit operator bool(ValidationResult result)
        {
            return !result.Failure;
        }
    }
}