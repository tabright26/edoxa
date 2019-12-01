// Filename: RuleBuilderOptionsExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq.Expressions;

using FluentValidation;

namespace eDoxa.Seedwork.Application.FluentValidation.Extensions
{
    public static class RuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, TEnumeration> WhenNotNull<T, TEnumeration>(
            this IRuleBuilderOptions<T, TEnumeration> validator,
            Expression<Func<T, TEnumeration>> expression
        )
        {
            return validator.When(type => expression.Compile()(type) != null);
        }
    }
}
