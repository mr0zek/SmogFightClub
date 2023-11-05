using System.Threading.Tasks;

namespace MediatR.Asynchronous
{
  public interface IInbox
  {
    Task<int> GetLastProcessedId(string module);
    Task SetProcessed(int id, string module);
  }
}