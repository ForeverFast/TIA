using System;
using System.Collections.Generic;

namespace TIA.Extentions
{
    public static class DataExtentions
    {
        public static void ReplaceReference<T>(IList<T> list, T newReference, Func<T, bool> predicate)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (predicate(list[i]))
                {
                    list[i] = newReference;
                    break;
                }
            }
        }

        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> recursiveFunc)
        {
            if (source != null)
            {
                foreach (var mainItem in source)
                {
                    yield return mainItem;

                    IEnumerable<T> recursiveSequence = (recursiveFunc(mainItem) ?? new T[] { }).SelectRecursive(recursiveFunc);

                    if (recursiveSequence != null)
                    {
                        foreach (var recursiveItem in recursiveSequence)
                        {
                            yield return recursiveItem;
                        }
                    }
                }
            }
        }
    }
}
