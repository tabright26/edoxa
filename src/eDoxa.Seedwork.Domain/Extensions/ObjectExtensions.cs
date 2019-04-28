// Filename: ObjectExtensions.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetGenericTypeName(this object obj)
        {
            return obj.GetType().GetGenericName();
        }
    }
}