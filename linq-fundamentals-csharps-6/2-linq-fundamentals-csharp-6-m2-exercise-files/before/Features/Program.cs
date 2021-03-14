using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {

           
            Employee[] developers = new Employee[]
            {
                new Employee { Id = 1, Name= "Scott" },
                new Employee { Id = 2, Name= "Chris" }
            };

            List<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };
            var query = developers.Where(n => n.Name.Length == 5)
                                  .OrderBy(n => n.Name);
            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;


            Console.WriteLine("Queries:");
            foreach (var employee in query2 )
                
            {
                Console.WriteLine(employee.Name);
            }
            Console.ReadLine();

            #region enumerators

            // //IEnumberable is the perfect interface for hiding
            // //the source of data

            //IEnumerable<Employee> developers = new Employee[]
            //{
            //      new Employee { Id = 1, Name= "Scott" },
            //      new Employee { Id = 2, Name= "Chris" }
            //};

            // IEnumerable<Employee> sales = new List<Employee>()
            //  {
            //      new Employee { Id = 3, Name = "Alex" }
            //  };

            //IEnumerator<Employee> enumerator = developers.GetEnumerator();

            ////extension method
            //Console.WriteLine(developers.Count());
            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine(enumerator.Current.Name);
            //}

            //foreach (var employee in developers.Where(n => n.Name.StartsWith("S")))
            //{
            //    Console.WriteLine(employee.Name);
            //}
            //Console.ReadLine();
            #endregion

            #region Func
            Func<int, int> square = x => x * x;
            Func<int, int, int> sum = (x, y) => x + y;
            Func<int, int, int> sumThenSquare = (a, b) =>
            {
                int summation = a + b;
                return square(summation);
            };

            Console.WriteLine(square(2));
            Console.WriteLine(sumThenSquare(2,3));
            Console.ReadLine();
            #endregion

            #region Action
            Action<int> write = x => Console.WriteLine($"From Action {x}");

            write(5);
            Console.ReadLine();
            #endregion
        }
    }
}
