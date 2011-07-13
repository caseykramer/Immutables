using System;
using System.Linq;
using System.Collections.Generic;

namespace Immutables.Collections
{
    public static class List
    {
        public static List<T> New<T>(params T[] args)
        {
            return new List<T>(args);
        }
    }

    public struct List<T> : IEnumerable<T>
    {
        public T Head
        {
            get
            {
                return _items[0];
            }
        }

        public List<T> Tail
        {
            get
            {
                var destArray = new T[_items.Length - 1];
                Array.Copy(_items, 1, destArray, 0, destArray.Length);
                return new List<T>(destArray);
            }
        }

        public int Length
        {
            get
            {
                return _items.Length;
            }
        }

        public T this[int index]
        {
            get
            {
                return _items[index];
            }
        }

        public static List<T> New
        {
            get { return new List<T>(); }
        }

        public List<T> this[int start, int end]
        {
            get
            {
                var destArray = new T[end - start];
                Array.Copy(_items, start, destArray, 0, destArray.Length);
                return new List<T>(destArray);
            }
        }

        private readonly T[] _items;
        public List(params T[] items)
        {
            _items = items;
        }

        public List(params List<T>[] args)
        {
            _items = args.SelectMany(t => t).ToArray();
        }

        public T FoldLeft(T initial, Func<T,T,T> folding)
        {
            Func<T, List<T>,T> reduce = null;
            reduce = ((i, l) =>
              {
                  if (l.Length > 1)
                      return reduce(folding(i, l.Head), l.Tail);
                  else
                      return folding(i,l.Head);                  
              });
            return reduce(initial,this);
        }

        public T ReduceLeft(Func<T, T, T> reducer)
        {
            return FoldLeft(default(T), reducer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _items)
                yield return item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public static List<T> operator +(List<T> listLeft, List<T> listRight)
        {
            return new List<T>(listLeft, listRight);
        }

        public static List<T> operator +(List<T> listLeft, T itemRight)
        {
            return new List<T>(listLeft, new List<T>(itemRight));
        }
    }
}
