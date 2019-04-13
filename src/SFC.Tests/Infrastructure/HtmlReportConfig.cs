using System;
using System.IO;
using TestStack.BDDfy.Reporters.Html;

namespace SFC.Tests.Infrastructure
{
  /// <summary>
  /// This overrides the default html report setting
  /// </summary>
  public class HtmlReportConfig : DefaultHtmlReportConfiguration
  {
    public override string OutputPath
    {
      get
      {
        var path = Path.Combine(base.OutputPath, "TestReports");
        // Ensure path exists
        Directory.CreateDirectory(path);
        return path;
      }
    }

    /// <summary>
    /// Change the output file name
    /// </summary>
    public override string OutputFileName => @"SFC.TestsReport.html";

    /// <summary>
    /// Change the report header to your project
    /// </summary>
    public override string ReportHeader => "Smog Fight Club";

    /// <summary>
    /// Change the report description
    /// </summary>
    public override string ReportDescription => "Here comes report of executed stories agains Smog Fight Club system";
  }
}
