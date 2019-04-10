// Filename: DictionaryExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Text;

namespace eDoxa.Notifications.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string Format(this string format, INotificationMetadata metadata)
        {
            var builder = new StringBuilder(format);

            var arguments = new Dictionary<string, int>();

            for (var index = 0; index < metadata.Count; index++)
            {
                var data = metadata.ElementAt(index);

                builder = builder.Replace("{" + data.Key + "}", "{" + index + "}");

                arguments.Add(data.Key, index);
            }

            return string.Format(builder.ToString(), metadata.OrderBy(argument => arguments[argument.Key]).Select(argument => argument.Value).ToArray());
        }
    }
}