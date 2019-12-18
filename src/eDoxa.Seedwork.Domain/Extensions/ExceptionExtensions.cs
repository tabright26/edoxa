// Filename: ExceptionExtensions.cs
// Date Created: 2019-12-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Runtime.ExceptionServices;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception Capture(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
            
            return exception;
        }
    }
}
