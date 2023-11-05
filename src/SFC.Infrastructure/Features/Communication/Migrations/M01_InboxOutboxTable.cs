using FluentMigrator;

namespace SFC.Infrastructure.Features.Communication.Migrations
{
  [Migration(202311011041)]
  public class M01_InboxOutboxTable : MediatR.Asynchronous.MsSql.Migrations.M01_InboxOutboxTable
  {
  }
}
