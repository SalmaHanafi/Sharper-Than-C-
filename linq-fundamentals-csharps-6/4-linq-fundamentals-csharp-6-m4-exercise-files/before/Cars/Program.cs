using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");
            //var query = cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name);
            var query = from car in cars
                        orderby car.Combined descending, car.Name
                        select car;

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select Car.ParseFromCsv(line);
            return query.ToList();
               
                 
                //File.ReadAllLines(path)
                //.Skip(1)
                //.Where(line => line.Length > 1)
                //.Select(Car.ParseFromCsv)
                //.ToList();
        }

    }
}
