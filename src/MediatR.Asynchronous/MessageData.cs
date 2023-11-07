namespace MediatR.Asynchronous
{
  public class MessageData
  {
    public string Data { get; set; }
    public string Type { get; set; }

    public int Id { get; set; }
    public MethodType MethodType { get; set; }

    public MessageData(int id, string data, string type, MethodType methodType)
    {
      Id = id;
      Type = type;
      Data = data;
      MethodType = (MethodType)methodType;
    }
    public MessageData(int id, string data, string type, Int16 methodType):this(id,data,type,(MethodType)methodType)
    {      
    }
  }
}