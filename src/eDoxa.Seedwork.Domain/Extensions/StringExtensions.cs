// Filename: StringExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    }
}
