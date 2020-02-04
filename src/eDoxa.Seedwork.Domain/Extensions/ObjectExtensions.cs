// Filename: InterfaceExtensions.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class ObjectExtensions
    {
        public static T Cast<T>(this object obj)
        {
            return (T) obj;
        }
    }
}
