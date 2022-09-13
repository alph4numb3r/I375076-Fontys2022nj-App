using DaySegments;
internal class Program
{
  /// <summary>
  /// Converts a given timestamp to a day segment
  /// </summary>
  /// <param name="args"></param>
  /// <arg name="timestamp">The timestamp to convert</arg>
  /// <returns>Exit code</returns>
  public static int Main(string[] args)
  {
    RootCommand rootcommand = new("Converts a given timestamp to a day segment");

    Argument<DateTime> TimestampArguemnt = new(
        name: "timestamp",
        description: "The timestamp to convert to a day segment.",
        parse: result =>
        {
          string resultString = (result.Tokens[0].Value) ?? "";
          if (resultString == ""
              || resultString == "''"
              || resultString.ToLowerInvariant() == "now")
          { return DateTime.Now; }
          else if (DateTime.TryParse(resultString, out DateTime resultDateTime))
          { return resultDateTime; }
          else
          {
            result.ErrorMessage = "Could not parse the timestamp argument.";
            return DateTime.UnixEpoch;
          }
        }
    );
    rootcommand.Add(TimestampArguemnt);

    rootcommand.SetHandler((timestamp) =>
    {
      var daysegment = (DaySegment)timestamp;
      Console.WriteLine(daysegment.ToString());
    }, TimestampArguemnt);

    return rootcommand.Invoke(args);
  }
}