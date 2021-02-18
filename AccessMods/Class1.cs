using System;
using System.Collections.Generic;
using System.Text;

namespace AccessMods
{
     public class Class1
    {
        private int prv = 1;
        public int pub;
        protected int prot;
        internal int intr;
        protected internal int pi;
        private protected int pp;

       public void PrintP()
        {
            Console.WriteLine("From Class1:");
            Console.WriteLine($"Private: {prv}");
        }
    }
}
