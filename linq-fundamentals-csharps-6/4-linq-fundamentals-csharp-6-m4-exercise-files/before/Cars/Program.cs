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
            //var top = cars.Where(a => a.Manufacturer == "BMW" && a.Year == 2016)
            //            .OrderByDescending(c => c.Combined)
            //            .ThenBy(c => c.Name)
            //            .First();

            var query = from car in cars
                        where car.Manufacturer == "BMW" && car.Year == 2016
                        orderby car.Combined descending, car.Name
                        select car;

            var top = cars.OrderByDescending(c => c.Combined)
                          .ThenBy(c => c.Name)
                          .First(a => a.Manufacturer == "BMW" && a.Year == 2016);

            Console.WriteLine($"From method syntax: {top.Name} : {top.Combined}");
            Console.WriteLine($"From query syntax: {query.First().Name} : {query.First().Combined}");
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
