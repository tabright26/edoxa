using System;
using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool TryGetValue<T, TModel>(this IDictionary<T, TModel> dictionary, Func<T, bool> predicate, out T? entity)
        where T : class
        {
            entity = dictionary.Keys.SingleOrDefault(predicate);

            return entity != null;
        }
    }
}
