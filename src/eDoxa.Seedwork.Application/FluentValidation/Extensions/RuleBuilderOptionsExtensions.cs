// Filename: RuleBuilderOptionsExtensions.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
