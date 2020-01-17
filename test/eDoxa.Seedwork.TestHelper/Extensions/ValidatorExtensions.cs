// Filename: ValidatorExtensions.cs
// Date Created: 2020-01-14
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using FluentValidation;
using FluentValidation.Results;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class ValidatorExtensions
    {
        public static IEnumerable<ValidationFailure> ShouldHaveValidationErrorFor<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            T request
        )
        where T : class, new()
        {
            var result = validator.Validate(request);

            return result.Errors.Where(failure => failure.PropertyName == expression.Name).ToList();
        }
    }
}
