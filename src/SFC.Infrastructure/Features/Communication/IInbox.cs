namespace SFC.Infrastructure.Features.Communication
{
  interface IInbox
  {
    int GetLastProcessedId(string module);
    void SetProcessed(int id, string module);
  }
}