using System.ComponentModel.DataAnnotations;

namespace DaySegments;

/// <summary>
/// A day segment is a part of a day, specifically either night, morning, afternoon or evening.
/// </summary>
public readonly record struct DaySegment
{
  /// <summary>
  /// Internal enumerator used for easy auto-conversion to a string.
  /// </summary>
  private enum daysegmentenum : byte { Night, Morning, Afternoon, Evening }

  /// <summary>
  /// Underlying byte value of the day segment.
  /// </summary>
  [Range(0, 3)]
  private readonly daysegmentenum _daysegment { get; init; } = 0;

  // Static values

  /// <summary>Represents a time between midnight and 6 AM</summary>
  public readonly static DaySegment Night = new
  (daysegmentenum.Night);

  /// <summary>Represents a time between 6 AM and noon</summary>
  public readonly static DaySegment Morning = new
  (daysegmentenum.Morning);

  /// <summary>Represents a time between noon and 6 PM</summary>
  public readonly static DaySegment Afternoon = new
  (daysegmentenum.Afternoon);

  /// <summary>Represents a time between 6 PM and midnight</summary>
  public readonly static DaySegment Evening = new
  (daysegmentenum.Evening);

  // Constructors

  /// <summary>
  /// Internal constructor to de-duplicate code.
  /// </summary>
  private DaySegment(daysegmentenum daysegment) => _daysegment = daysegment;

  /// <summary>
  /// Constructs a <see cref="DaySegment"/> from a given <see cref="DateTime"/>.
  /// </summary>
  /// <param name="timestamp"></param>
  public DaySegment(DateTime timestamp) : this
    ((daysegmentenum)(timestamp.Hour / 6))
  { }

  /// <summary>
  /// Attempts to construct a <see cref="DaySegment"/> from the given <see cref="string"/>.
  /// </summary>
  /// <param name="segment">The <see cref="string"/> to parse from</param>
  /// <exception cref="ArgumentException">Thrown when the <see cref="string"/> cannot be parsed</exception>
  public DaySegment(string segment) : this(
      segment.ToLowerInvariant() switch
      {
        "night" => daysegmentenum.Night,
        "morning" => daysegmentenum.Morning,
        "afternoon" => daysegmentenum.Afternoon,
        "evening" => daysegmentenum.Evening,

        _ => throw new ArgumentException
          ("Invalid day segment.", nameof(segment))
      }
  )
  { }

  // Conversions

  /// <inheritdoc cref="DaySegment(DateTime)"/>
  /// <seealso cref="DaySegment(DateTime)"/>
  /// <param name="timestamp"><see cref="DateTime"/> to convert from.</param>
  public static explicit operator DaySegment(DateTime timestamp) =>
    new(timestamp);

  /// <summary>
  /// Converts the value of this <see cref="DaySegment"/> to its equivalent <see cref="byte"/> representation.
  /// </summary>
  /// <seealso cref="ToNumeric()"/>
  public static implicit operator byte(DaySegment daysegment) =>
    daysegment.ToNumeric();

  /// <inheritdoc cref="implicit operator byte"/>
  /// <seealso cref="implicit operator byte"/>
  public byte ToNumeric() =>
    (byte)_daysegment;

  /// <summary>
  /// Converts the value of this <see cref="DaySegment"/> to its equivalent <see cref="string"/> representation.
  /// </summary>
  /// <seealso cref="implicit operator string"/>
  /// <seealso cref="ToString(string)"/>
  /// <returns>The string representation of the value of this instance.</returns>
  public override string ToString() =>
    _daysegment.ToString();

  /// <summary>
  /// Converts the value of this <see cref="DaySegment"/> to its equivalent <see cref="string"/> representation using the specified format.
  /// </summary>
  /// <seealso cref="implicit operator string"/>
  /// <seealso cref="ToString()"/>
  /// <param name="format">A format string.</param>
  /// <returns>The string representation of the value of this instance as specified by format.</returns>
  public string ToString(string format) =>
    _daysegment.ToString(format);

  /// <inheritdoc cref="ToString()"/>
  /// <seealso cref="ToString()"/>
  /// <seealso cref="ToString(string)"/>
  public static implicit operator string(DaySegment daysegment) =>
    daysegment.ToString();
}