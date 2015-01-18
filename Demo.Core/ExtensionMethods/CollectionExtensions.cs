using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Demo.Core.ExtensionMethods
{
    public static class CollectionExtensions
    {
        [DebuggerStepThrough]
        public static void AddAll<T>(
            this ICollection<T> instance,
            IEnumerable<T> source)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            source.Each(instance.Add);
        }
    }
}