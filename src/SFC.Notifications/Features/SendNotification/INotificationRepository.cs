using System;
using SFC.SharedKernel;

namespace SFC.Notifications
{
  internal interface INotificationRepository 
  {
    void Add(Email email, string title, string body, DateTime date, LoginName loginName);
  }
}