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
            #region filtering and ordering 
            //var top = cars.Where(a => a.Manufacturer == "BMW" && a.Year == 2016)
            //            .OrderByDescending(c => c.Combined)
            //            .ThenBy(c => c.Name)
            //            .First();
            //var top = cars.OrderByDescending(c => c.Combined)
            //              .ThenBy(c => c.Name)
            //              .First(a => a.Manufacturer == "BMW" && a.Year == 2016);

            //var query = from car in cars
            //            where car.Manufacturer == "BMW" && car.Year == 2016
            //            orderby car.Combined descending, car.Name
            //            select car;
            //Console.WriteLine($"From query syntax: {query.First().Name} : {query.First().Combined}");
            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Name} : {car.Combined}");
            //}

            //var av = cars.Any(c => c.Year == 2016);
            //Console.WriteLine(av);
            //Console.WriteLine($"From method syntax: {top.Name} : {top.Combined}");
            #endregion

            var query = from car in cars
                        where car.Manufacturer == "bmw" && car.Year == 2016
                        orderby car.Combined descending, car.Name
                        select new
                        {
                            car.Manufacturer,
                            car.Name,
                            car.Combined

                        };

            //looking at the string as a sequence of characters
            var result = cars.SelectMany(c => c.Name)
                              .OrderBy(c => c)
                              .GroupBy(c => c);
                             
            foreach (var character in result)
            {
                Console.WriteLine($"{character.Key} : {character.Count()}");
            }
        
        }   
        private static List<Car> ProcessFile(string path)
        {
            //var query = from line in File.ReadAllLines(path).Skip(1)
            //            where line.Length > 1
            //            select Car.ParseFromCsv(line);

            var query = File.ReadAllLines(path)
                            .Skip(1)
                            .Where(l => l.Length > 1)
                            .ToCar();
            return query.ToList();
               
                 
                //File.ReadAllLines(path)
                //.Skip(1)
                //.Where(line => line.Length > 1)
                //.Select(Car.ParseFromCsv)
                //.ToList();
        }

    }
}
