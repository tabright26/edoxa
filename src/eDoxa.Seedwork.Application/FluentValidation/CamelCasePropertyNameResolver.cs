// Filename: CamelCasePropertyNameResolver.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq.Expressions;
using System.Reflection;

using eDoxa.Seedwork.Domain.Extensions;

using FluentValidation.Internal;

namespace eDoxa.Seedwork.Application.FluentValidation
{
    public class CamelCasePropertyNameResolver
    {
        public static string? ResolvePropertyName(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            return DefaultPropertyNameResolver(type, memberInfo, expression).ToCamelCase();
        }

        private static string? DefaultPropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression != null)
            {
                var chain = PropertyChain.FromExpression(expression);

                if (chain.Count > 0)
                {
                    return chain.ToString();
                }
            }

            if (memberInfo != null)
            {
                return memberInfo.Name;
            }

            return null;
        }
    }
}
