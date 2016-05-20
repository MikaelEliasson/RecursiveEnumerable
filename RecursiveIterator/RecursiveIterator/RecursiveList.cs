using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecursiveIterator
{
    public class RecursiveList<T> : IEnumerable<T>
    {
        private Func<T, IEnumerable<T>> navigator;
        private T start;

        public RecursiveList(T start, Func<T, T> navigator)
        {
            this.start = start;
            this.navigator = x => {
                var item = navigator(x);
                return item == null ? Enumerable.Empty<T>() : Enumerable.Repeat(navigator(x), 1);
             };
        }

        public RecursiveList(T start, Func<T, IEnumerable<T>> navigator)
        {
            this.start = start;
            this.navigator = navigator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Any())
            {
                var item = queue.Dequeue();
                var children = navigator(item) ?? new List<T>();
                foreach (var child in children)
                {
                    queue.Enqueue(child);
                }
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
