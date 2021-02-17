using System;
using System.Collections.Generic;
using System.Text;

//Both class and extension method need to be static
namespace Extensions
{
    public static class IntExtensions
    {
        public static Boolean IsGreaterThan (this int i, int value)
        {
            return i > value;
        }
    }
}
