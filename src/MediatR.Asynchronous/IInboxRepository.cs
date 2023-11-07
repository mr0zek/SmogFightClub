using System.Threading.Tasks;

namespace MediatR.Asynchronous
{
  public interface IInboxRepository
  {
    Task<int> GetLastProcessedId(string module);
    Task SetProcessed(int id, string module);
  }
}