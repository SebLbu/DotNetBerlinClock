using System;
using System.Globalization;

namespace BerlinClock
{
    /// <inheritdoc />
    public class TimeConverter : ITimeConverter
    {
        private readonly IClock clock;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeConverter"/> class.
        /// </summary>
        public TimeConverter()
        {
            // todo: factory to instantiate IClock<T> injected with DI
            this.clock = new BerlinClock();
        }

        /// <summary>
        /// Converts a given time into a given clock's time
        /// </summary>
        /// <param name="timeToRead">Time in format HH:mm:ss Range must be between 00:00:00 and 23:59:59</param>
        /// <returns>The time as read in the given clock</returns>
        public string ConvertTime(string timeToRead)
        {
            var timeToConvert = this.ParseTime(timeToRead);
            return this.clock.ReadTime(timeToConvert);
        }

        /// <summary>
        /// Parses the time into a Datetime field to pass onto the clock.
        /// </summary>
        /// <param name="timeToParse">
        /// The time to parse
        /// </param>
        /// <returns>
        /// The time in a <see cref="DateTime"/> field.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// When format or range are invalid. Accepted format is HH:mm:ss and range must be between 00:00:00 and 23:59:59
        /// </exception>
        private DateTime ParseTime(string timeToParse)
        {
            if (DateTime.TryParseExact(timeToParse, "HH:mm:ss", new CultureInfo("CH-DE"), DateTimeStyles.None, out var convertedTime))
            {
                return convertedTime;
            }

            throw new ArgumentException($"{timeToParse} is not a valid time. Expected format is HH:mm:ss and range must be between 00:00:00 and 23:59:59");
        }
    }
}
