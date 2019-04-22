// Filename: ExpressionExtensions.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using AutoMapper;

using eDoxa.Functional.Option;

namespace eDoxa.AutoMapper.Extensions
{
    public static class MemberConfigurationExpressionExtensions
    {
        public static void OptionalMapFrom<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> config,
            Expression<Func<TSource, Option<TMember>>> expression)
        {
            config.MapFrom(source => expression.Compile()(source).Default());
        }
    }
}