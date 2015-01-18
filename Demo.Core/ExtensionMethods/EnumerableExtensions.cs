using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Demo.Core.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void Each<T>(
            this IEnumerable<T> instance,
            Action<T> action)
        {
            EachWithIndex(instance, (item, index) => action(item));
        }

        [DebuggerStepThrough]
        public static void EachWithIndex<T>(
            this IEnumerable<T> instance,
            Action<T, int> action)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            using (var iterator = instance.GetEnumerator())
            {
                var index = 0;

                while (iterator.MoveNext())
                {
                    action(iterator.Current, index++);
                }
            }
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> Tap<T>(
            this IEnumerable<T> instance,
            Action<IEnumerable<T>> action)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            // ReSharper disable once PossibleMultipleEnumeration
            action(instance);

            // ReSharper disable once PossibleMultipleEnumeration
            return instance;
        } 
    }
}