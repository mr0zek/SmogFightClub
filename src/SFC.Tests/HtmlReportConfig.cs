using TestStack.BDDfy.Reporters.Html;

namespace SFC.Tests
{
  /// <summary>
  /// This overrides the default html report setting
  /// </summary>
  public class HtmlReportConfig : DefaultHtmlReportConfiguration
  {
    /// <summary>
    /// Change the output file name
    /// </summary>
    public override string OutputFileName
    {
      get
      {
        return "SFC.TestsReport.html";
      }
    }

    /// <summary>
    /// Change the report header to your project
    /// </summary>
    public override string ReportHeader
    {
      get
      {
        return "Smog Fight Club";
      }
    }

    /// <summary>
    /// Change the report description
    /// </summary>
    public override string ReportDescription
    {
      get
      {
        return "Here comes report of executed stories agains Smog Fight Club system";
      }
    }    
  }
}
