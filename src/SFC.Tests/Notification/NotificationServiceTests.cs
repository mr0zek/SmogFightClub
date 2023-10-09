using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Autofac;
using Microsoft.Extensions.Configuration;
using SFC.Notifications;
using SFC.SharedKernel;
using System.Linq;
using SFC.Tests.Mocks;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Infrastructure;
using SFC.Notifications.Features.GetSendNotificationsCount.Contract;
using Microsoft.AspNetCore.Builder;

namespace SFC.Tests.Notification
{

  [Collection("Sequential")]
  public class NotificationServiceTests 
  {
    private readonly IContainer _container;

    public NotificationServiceTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      SFC.Infrastructure.DbMigrations.Run(connectionString);

      var builder = new ContainerBuilder();
      builder.RegisterModule(new AutofacNotificationsModule());
      builder.RegisterModule(new AutofacInfrastructureModule());
      builder.RegisterInstance(new ConnectionString(connectionString));
      builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
      builder.RegisterType<TestDateTimeProvider>().AsImplementedInterfaces();
      builder.RegisterInstance(new TestEventHandler<NotificationSentEvent>()).AsImplementedInterfaces();

      _container = builder.Build();
    }    

    [Fact]
    public void Send_notification_suuccess_scenaio()
    {
      // Arrange
      var setNotificationEmail = _container.Resolve<ICommandHandler<SetNotificationEmailCommand>>();
      var sendNotification = _container.Resolve<ICommandHandler<SendNotificationCommand>>();
      var query = _container.Resolve<IQuery>();

      LoginName loginName = "ala" + Guid.NewGuid();
      string notificationType = "type1";
      string email = "example@exmaple.com";

      setNotificationEmail.Handle(new SetNotificationEmailCommand(
        email,
        loginName));
      
      // Act
      sendNotification.Handle(new SendNotificationCommand()
      {
        LoginName = loginName,
        Title = "title",
        Body = "body",
        NotificationType = notificationType
      });

      // Assert
      var result = query.Query(
        new GetSendNotificationsCountRequest()
        {
          NotificationType = notificationType,
          LoginNames = new[] { loginName }
        });

      var entry = result.Result.FirstOrDefault(f=>f.LoginName == loginName);

      Assert.Equal(loginName,entry.LoginName);
      Assert.Equal(1,entry.Count);
      Assert.Single(TestSmtpClient.SentEmails.Where(f=>f.Email == email));
      Assert.Single(TestEventHandler<NotificationSentEvent>.Events);
    }
  }
}
