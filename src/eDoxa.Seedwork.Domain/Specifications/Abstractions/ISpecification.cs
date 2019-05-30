// Filename: ISpecification.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

namespace eDoxa.Seedwork.Domain.Specifications.Abstractions
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();

        bool IsSatisfiedBy(T entity);

        ISpecification<T> And(ISpecification<T> specification);

        ISpecification<T> And(Expression<Func<T, bool>> right);

        ISpecification<T> Or(ISpecification<T> specification);

        ISpecification<T> Or(Expression<Func<T, bool>> right);

        ISpecification<T> Not();
    }
}