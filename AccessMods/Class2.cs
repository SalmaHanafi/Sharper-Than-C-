using System;
using System.Collections.Generic;
using System.Text;
using AccessMods;

namespace AccessMods1
{
    public class Class2:Class1
    {
        
        public void Assign()
        {
            prot = 8; //because of the derivation from Class1
            pub = 9; //public accessable from everywhere
            pp = 10; //private protected because of the derivation
            pi = 11; // protected internal because of the derivation

            //internal wouldn't work here because it is another assembly
            // private won't work here because it is not the same class
            Console.WriteLine("From Class2:");
            Console.WriteLine($"Protected: {prot}");
            Console.WriteLine($"Protected Internal: {pi}" );
        }

    }
}
