// Filename: ReferenceOwnershipBuilderExtensions.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Seedwork.Domain.Aggregate;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ReferenceOwnershipBuilderExtensions
    {
        public static PropertyBuilder<TEnumeration> Enumeration<TValueObject, TObject, TEnumeration>(
            this ReferenceOwnershipBuilder<TValueObject, TObject> builder,
            Expression<Func<TObject, TEnumeration>> expression
        )
        where TObject : class
        where TValueObject : ValueObject
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return builder.Property(expression).HasConversion(enumeration => enumeration.Value, value => Enumeration<TEnumeration>.FromValue(value));
        }
    }
}
