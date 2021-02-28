using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    class User
    {
        private INotificationService _notificationService;
        public string UserName { get; set; }
        public User(string name, INotificationService notificationService)
        {
            UserName = name;
            _notificationService = notificationService;


        }
        public void ChangeUserName(string newUserName)
        {
            UserName = newUserName;
            _notificationService.NotifyUserNameChange(this);
        }

    }
}
