using System.Threading.Tasks;

namespace SFC.Processes.Features.UserRegistrationSaga
{
  public interface ISagaRepository
  {
    Task Save(string id, object data);
    Task<T?> Get<T>(string id) where T : class;
  }
}