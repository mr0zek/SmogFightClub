using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Communication.AsyncEventProcessing
{
  interface IInbox
  {
    Task<int> GetLastProcessedId(string module);
    Task SetProcessed(int id, string module);
  }
}