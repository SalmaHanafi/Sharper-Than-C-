using System;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            ProjectionQuery();
            Console.ReadKey();
        }
        private static void InsertNinja()
        {
            var ninja = new Ninja
            {
                Name = "Sampson",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2009, 10, 20),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();

            }

        }
        private static void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }
            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                //update happens here
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        private static void RetrieveDataWithFind()
        {
            var keyval = 4;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //check if key already exists before querying the db
                var ninja = context.Ninjas.Find(keyval);
                Console.WriteLine($"After Find#1: {ninja.Name}");

                var someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine($"After Find#2: {someNinja.Name}");
                ninja = null;
            }
        }

        private static void RetrieveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas");
                //sql query is just a query, the reason it got executed is that
                // we enumerated with foreach, otherwise we have to use one the LINQ
                //execution methods at the end like: context.Ninjas.SqlQuery("exec GetOldNinjas").ToList()
                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name);
                }
            }
        }

        private static void DeleteNinja()
        {
            Ninja ninja;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
                //context.Ninjas.Remove(ninja);
                //context.SaveChanges();
            }

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        //two trips to the db; which is a waste
        private static void DeleteNinjaWithKeyValue()
        {
            var keyval = 1;
            using (var context = new NinjaContext())
            {
                //round 1: retrieving ninja
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                //round 2
                context.Ninjas.Remove(ninja);
                context.SaveChanges();

            }
        }

        //stored procedure similar to sql query
        private static void DeleteNinjaViaStoredProcedure()
        {
            var keyval = 4;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand("exec DeleteNinjaViaId{0}", keyval);
            }
        }
        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja
                {
                    Name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };
                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool,

                };
                var spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }

        }

        private static void simpleninjagraphquery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //var ninjas = context.Ninjas.Include(n => n.EquipmentOwned)
                //    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));

                var ninja = context.Ninjas
                           .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                Console.WriteLine("Ninja Retrieved:" + ninja.Name);
                context.Entry(ninja).Collection(n => n.EquipmentOwned).Load();

                //Console.WriteLine("Ninja Equipment Count: {0}:", ninja.EquipmentOwned.Count());


            }
        }
        private static void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas
                    .Select(n => new { n.Name, n.DateOfBirth, n.EquipmentOwned })
                    .ToList();

            }
        }
    }
}
