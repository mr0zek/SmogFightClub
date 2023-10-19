using SFC.Infrastructure.Interfaces;

namespace SFC.Infrastructure.Interfaces.Database
{
  public interface IDatabaseMigrator
  {
    void Run();
  }
}