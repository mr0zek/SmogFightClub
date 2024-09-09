using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediatR.Asynchronous
{
  public interface IOutboxRepository
  {
    Task Add(MessageData eventData);
    Task<IEnumerable<MessageData>> Get(int count, string moduleName);
  }
}