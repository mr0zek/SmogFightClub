using RestEase;

namespace SFC.Tests
{
  public class PostAccountResponse
  {
    [Header("Location")]
    public string Location { get; set; }
  }
}