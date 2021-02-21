using System;
using System.Collections;
using System.Collections.Generic;

namespace Enumerating
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = new int[] { 1, 2, 3 };
            foreach (var num in nums)
            {
                Console.WriteLine(num);
            }
            var enumerator = nums.GetEnumerator();
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }



            var infEnumerable = new MyInfiniteEnumerable();
            foreach(var i in infEnumerable)
            {
                Console.WriteLine(i);

            }
        }
    }
    public class MyInfiniteEnumerable : IEnumerable<int>
    {
        public IEnumerator GetEnumerator()
        {
            return new MyInfiniteEnumerator();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new MyInfiniteEnumerator();
        }
    }
    public class MyInfiniteEnumerator : IEnumerator<int>
    {
        public int Current { get; private set; } = 0;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
     
        }

        public bool MoveNext()
        {
            Current++;
            return true;
        }

        public void Reset()
        {
            Current = 0;
        }
    }
}
