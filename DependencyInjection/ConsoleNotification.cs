using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    class ConsoleNotification: INotificationService
    {
        public void NotifyUserNameChange( User user)
        {
            Console.WriteLine($"Username has been changed to: {user.UserName}");
        }
    }
}
