using System;
using System.Collections.Generic;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create an instance of SQLServer
            var compiler = new SqlServerCompiler();

            var query = new Query("Users").Where("Id", 1).Where("Status", "Active");

            SqlResult result = compiler.Compile(query);

            string sql = result.Sql;
            List<object> bindings = result.Bindings; // [ 1, "Active" ]
            Console.WriteLine(bindings[1]);
        }
    }
}
