namespace MediatR.Asynchronous
{
  public class MessageData
  {
    public string Data { get; set; }
    public DateTime Date { get; }
    public string Type { get; set; }

    public int Id { get; set; }
    public MethodType MethodType { get; set; }

    public MessageData(int id, string data, DateTime date, string type, MethodType methodType)
    {
      Id = id;
      Type = type;
      Data = data;
      Date = date;
      MethodType = (MethodType)methodType;
    }
    public MessageData(int id, string data, DateTime date, string type, Int16 methodType):this(id,data,date,type,(MethodType)methodType)
    {      
    }
  }
}