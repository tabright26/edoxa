// Filename: ValidationFailureExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using FluentValidation.Results;

namespace eDoxa.Seedwork.Application.Validations.Extensions
{
    public static class ValidationFailureExtensions
    {
        public static ValidationResult ToResult(this ValidationFailure validationFailure)
        {
            return new ValidationResult(
                new HashSet<ValidationFailure>
                {
                    validationFailure
                }
            );
        }
    }
}
