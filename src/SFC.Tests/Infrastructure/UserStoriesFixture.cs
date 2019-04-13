using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace SFC.Tests
{
  public class UserStoriesFixture 
  {
    public UserStoriesFixture()
    {      
      Configurator.BatchProcessors.Add(new HtmlReporter(new HtmlReportConfig(), new MetroReportBuilder()));
    }
  }
}
