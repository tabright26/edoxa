// Filename: ExpressionSpecificationVisitor.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq.Expressions;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Specifications
{
    internal sealed class ExpressionSpecificationVisitor : ExpressionVisitor
    {
        private readonly Expression _from;
        private readonly Expression _to;

        public ExpressionSpecificationVisitor(Expression from, Expression to)
        {
            _from = from;
            _to = to;
        }

        public override Expression Visit([NotNull] Expression node)
        {
            return node == _from ? _to : base.Visit(node);
        }
    }
}