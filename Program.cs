internal class Program
{
  enum DaySegment : byte { Night, Morning, Afternoon, Evening }
  private static int Main(string[] args)
  {    
    DateTime timestamp;
    if (args.Length > 0) 
    {
      if (!DateTime.TryParse(args[0], out timestamp)){
        Console.WriteLine("Invalid timestamp given: {0}", args[0]);
        return 1;
      }
    }else{
      timestamp = DateTime.Now;
    }
    Console.WriteLine("Hello, World!");
    Console.WriteLine("Good {0}! It is currently {1}", GetDaySegment(timestamp), timestamp.ToShortTimeString());
    return 0;
  }
  /// <summary>
  /// Gets the day segment of the given timestamp, defaulting to now if no timestamp is given.
  /// </summary>
  /// <param name="timestamp">The timestamp to get the day segment for. Defaults to now if not specified.</param>
  private static DaySegment GetDaySegment(DateTime? timestamp = null) {
    timestamp ??= DateTime.Now;
    return (DaySegment) (timestamp.Value.Hour / 6);
  }
}