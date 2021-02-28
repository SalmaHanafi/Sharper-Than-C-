using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    interface INotificationService
    {
        void NotifyUserNameChange(User user);
    }
}
