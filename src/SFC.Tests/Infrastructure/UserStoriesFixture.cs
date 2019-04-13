using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace SFC.Tests.Infrastructure
{
  public class UserStoriesFixture 
  {
    public UserStoriesFixture()
    {
      Configurator.BatchProcessors.HtmlReport.Disable();
      Configurator.BatchProcessors.Add(new HtmlReporter(new HtmlReportConfig(), new MetroReportBuilder()));
    }
  }
}
