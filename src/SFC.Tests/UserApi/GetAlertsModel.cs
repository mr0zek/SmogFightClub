using System.Collections.Generic;

namespace SFC.Tests.UserApi
{
  public class GetAlertsModel
  {
    public IEnumerable<GetAlertModel> Alerts { get; set; }    
  }
}