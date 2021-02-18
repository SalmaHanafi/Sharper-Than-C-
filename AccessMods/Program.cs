using System;
using AccessMods;

namespace AccessMods1
{
    class Program:Class1
    {
          static void Main(string[] args)
        {
            Class1 c1 = new Class1();
            Class2 c2 = new Class2();
            c1.PrintP();
            c2.Assign();
            c1.intr = 12;
            Console.WriteLine("From Main:");
            Console.WriteLine($"public:{c2.pub}");
            Console.WriteLine($"Internal:{c1.intr}");
            
            Console.WriteLine($"ProtectedInternal:{c1.pi}");
        }
    }
}
