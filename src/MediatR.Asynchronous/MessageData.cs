namespace MediatR.Asynchronous
{
  public class MessageData
  {
    public string Data { get; set; }
    public string Type { get; set; }

    public int Id { get; set; }
    public MethodType MethodType { get; set; }
  }
}