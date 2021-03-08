using System;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System.Data.Entity;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            InsertNinja();
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
    }
}
