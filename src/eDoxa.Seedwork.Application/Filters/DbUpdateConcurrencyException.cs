// Filename: DbUpdateConcurrencyException.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.Filters;

namespace eDoxa.Seedwork.Application.Filters
{
    public sealed class DbUpdateConcurrencyException : IExceptionFilter
    {
        public void OnException([NotNull] ExceptionContext context)
        {
        }
    }
}