using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecursiveIterator
{
    public class RecursiveList<T> : IEnumerable<T>
    {
        public class MetaData
        {
            public T Item { get; set; }
            public int Index { get; set; }
            public MetaData Previous { get; set; }
        }

        private Func<T, MetaData, IEnumerable<T>> navigator;
        private T start;

        public RecursiveList(T start, Func<T, T> navigator)
        {
            this.start = start;
            this.navigator = (x, meta) => {
                var item = navigator(x);
                return item == null ? Enumerable.Empty<T>() : Enumerable.Repeat(item, 1);
             };
        }

        public RecursiveList(T start, Func<T, MetaData, T> navigator)
        {
            this.start = start;
            this.navigator = (x, meta) => {
                var item = navigator(x, meta);
                return item == null ? Enumerable.Empty<T>() : Enumerable.Repeat(item, 1);
            };
        }

        public RecursiveList(T start, Func<T, IEnumerable<T>> navigator)
        {
            this.start = start;
            this.navigator = (item, meta) => navigator(item);
        }

        public RecursiveList(T start, Func<T, MetaData, IEnumerable<T>> navigator)
        {
            this.start = start;
            this.navigator = navigator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<T>();
            queue.Enqueue(start);
            var meta = new MetaData { Index = 0, Previous = null };

            while (queue.Any())
            {
                var item = queue.Dequeue();
                meta.Item = item;
                var children = navigator(item, meta) ?? new List<T>();
                foreach (var child in children)
                {
                    queue.Enqueue(child);
                }
                yield return item;
                meta = new MetaData { Index = meta.Index + 1, Previous = meta };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
