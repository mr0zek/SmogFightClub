using System;
using System.Text;

namespace SFC.Infrastructure.Interfaces.TimeDependency
{
  /// <summary>
  /// <para>
  /// <br/>   *    *       *        *      *
  /// <br/>Minute Hour DayOfMonth Month DayOfWeek
  /// </para>
  /// <para>
  /// <br/>"*"	- any value
  /// <br/>","	- value list separator
  /// <br/>"-"	- range of values
  /// <br/>"/"	- step values
  /// </para>
  /// <para>
  /// <br/>Examples:
  /// <br/>1. */5 * * * * - At every 5th minute
  /// <br/>2. 1 * * * *   - At minute 1
  /// <br/>3. 0 0 1 * *   - At 00:00 on day-of-month 1.
  /// <br/>4. 1-3 * * * * - At every minute from 1 through 3
  /// <br/>5. 0 0 */1 * * - At 00:00 on every day-of-month
  /// </para>
  /// </summary>
  public class CrontabAttribute : Attribute
  {
    public CrontabAttribute(string crontab)
    {
      Crontab = crontab;
    }

    public string Crontab { get; }
  }
}