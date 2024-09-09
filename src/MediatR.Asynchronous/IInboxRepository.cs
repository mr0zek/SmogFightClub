using System.Threading.Tasks;

namespace MediatR.Asynchronous
{
  public interface IInboxRepository
  {
    Task<bool> SetProcessed(int id, DateTime date, string module);
  }
}