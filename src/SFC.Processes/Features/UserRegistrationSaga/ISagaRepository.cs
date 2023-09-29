namespace SFC.Processes.Features.UserRegistrationSaga
{
  public interface ISagaRepository
  {
    void Save(string id, object data);
    T Get<T>(string id) where T : class;
  }
}