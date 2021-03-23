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
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            #region joins
            //using anonymous function to join on more than 1 prop

            //var query =
            //    from car in cars
            //    join manufacturer in manufacturers
            //    on new { car.Manufacturer, car.Year }
            //    equals new { Manufacturer = manufacturer.Name , manufacturer.Year}
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };


            //var query = cars.Join(manufacturers,
            //                      c => new { c.Manufacturer, c.Year },
            //                      m => new { Manufacturer = m.Name, m.Year },
            //                      (c, m) => new
            //                      {
            //                          m.Headquarters,
            //                          c.Name,
            //                          c.Combined
            //                      })
            //                .OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);


            //foreach (var c in query.Take(10))
            //{
            //    Console.WriteLine($"{c.Headquarters} {c.Name} : {c.Combined}");
            //    }

            #endregion

            #region grouping
            //var query =
            //    from car in cars
            //    group car by car.Manufacturer.ToUpper() into manufacturer
            //    orderby manufacturer.Key
            //    select manufacturer;

            //var query =
            //    cars.GroupBy(c => c.Manufacturer.ToUpper())
            //                .OrderBy(g => g.Key);
            //foreach (var group in query)
            //{
            //    Console.WriteLine($"***{group.Key}***");
            //    foreach (var car in group.OrderBy(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}
            #endregion

            #region groupjoin

            //var query =
            //    from manufacturer in manufacturers
            //    join car in cars on manufacturer.Name equals car.Manufacturer
            //        into carGroup
            //    orderby manufacturer.Name
            //    select new
            //    {
            //        Manufacturer = manufacturer,
            //        Cars = carGroup
            //    };

            //var query = manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer, (m, g) =>
            //        new
            //        {
            //            Manufacturer = m,
            //            Cars = g
            //        })
            //    .OrderBy(m => m.Manufacturer.Name);

            //    foreach (var group in query)
            //    {
            //        Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
            //        foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
            //        {
            //            Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //        }
            //    }

            #endregion

            var query = cars.Join(manufacturers,
                                  c => c.Manufacturer,
                                  m => m.Name,
                                  (c, m) => new
                                  {
                                      m.Headquarters,
                                      c.Name,
                                      c.Combined
                                  }).
                GroupBy(m => m.Headquarters);

            foreach (var group in query)
                {
                    Console.WriteLine($"{group.Key}");
                    foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                    {
                        Console.WriteLine($"\t{car.Name} : {car.Combined}");
                    }
                }

        }

        private static List<Car> ProcessCars(string path)
        {
            var query =

                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            return query.ToList();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                   File.ReadAllLines(path)
                       .Where(l => l.Length > 1)
                       .Select(l =>
                       {
                           var columns = l.Split(',');
                           return new Manufacturer
                           {
                               Name = columns[0],
                               Headquarters = columns[1],
                               Year = int.Parse(columns[2])
                           };
                       });
            return query.ToList();
        }
    }

    public static class CarExtensions
    {        
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
