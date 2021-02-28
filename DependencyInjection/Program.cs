using System;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var notificationService = new ConsoleNotification();
            var user1 = new User("Salma", notificationService);
            user1.ChangeUserName("Salma Hanafi");

            Console.ReadKey();
        }
    }
}
