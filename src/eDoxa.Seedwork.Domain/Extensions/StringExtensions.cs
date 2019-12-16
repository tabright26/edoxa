// Filename: StringExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Globalization;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class StringExtensions
    {
        public static TEntityId ParseEntityId<TEntityId>(this string entityId)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return EntityId<TEntityId>.Parse(entityId);
        }

        public static TStringId ParseStringId<TStringId>(this string stringId)
        where TStringId : StringId<TStringId>, new()
        {
            return StringId<TStringId>.Parse(stringId);
        }

        public static string? ToCamelCase(this string? value)
        {
            if (string.IsNullOrEmpty(value) || !char.IsUpper(value![0]))
            {
                return value;
            }

            var chars = value.ToCharArray();

            for (var index = 0; index < chars.Length; index++)
            {
                if (index == 1 && !char.IsUpper(chars[index]))
                {
                    break;
                }

                var hasNext = index + 1 < chars.Length;

                if (index > 0 && hasNext && !char.IsUpper(chars[index + 1]))
                {
                    break;
                }

                chars[index] = char.ToLower(chars[index], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }
    }
}
