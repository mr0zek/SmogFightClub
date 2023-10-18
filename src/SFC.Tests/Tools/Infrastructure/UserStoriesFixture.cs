using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace SFC.Tests.Tools.Infrastructure
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
