using System;
using System.Globalization;
using System.Text;

namespace BerlinClock.Classes
{
    /// <summary>
    /// The clock implementation to read a given time in Berlin clock format
    /// </summary>
    public class BerlinClock : IClock
    {
        private const char LightYellow = 'Y';
        private const char LightRed = 'R';
        private const char LightOff = 'O';
        
        private readonly DateTime timeToConvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="BerlinClock"/> class.
        /// </summary>
        /// <param name="timeToDisplay">
        /// The time to display in Berlin clock format.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Exception thrown if format is not HH:mm:ss and range must be between 00:00:00 and 23:59:59
        /// </exception>
        public BerlinClock(string timeToDisplay)
        {
            if (!DateTime.TryParseExact(timeToDisplay, "HH:mm:ss", new CultureInfo("CH-DE"), DateTimeStyles.None, out this.timeToConvert))
            {
                throw new ArgumentException($"Argument {nameof(timeToDisplay)} with value {timeToDisplay} is not a valid time. Expected format is HH:mm:ss and range must be between 00:00:00 and 23:59:59");
            }
        }
        
        /// <summary>
        /// Display's the time in <see cref="T:BerlinClock.Classes.BerlinClock" /> format
        /// </summary>
        /// <returns>
        /// A string representing the time as displayed on a Berlin clock
        /// </returns>
        public override string ToString()
        {
            var newLine = Environment.NewLine;

            return $@"{this.DisplaySeconds()}{newLine}{this.DisplayHours()}{newLine}{this.DisplayMinutes()}";
        }

        /// <summary>
        /// Returns the string to display seconds in <see cref="BerlinClock"/> format
        /// When the number of seconds is even, the light will be yellow, else the light is switched off
        /// </summary>
        /// <returns>
        /// A string representing the state of seconds.
        /// </returns>
        private string DisplaySeconds()
        {
            const int LightsToDisplay = 1;
            var isValueOdd = this.timeToConvert.Second % 2 != 0;
            return isValueOdd ? ConvertToLight(LightOff, LightsToDisplay) : ConvertToLight(LightYellow, LightsToDisplay);
        }

        /// <summary>
        /// Returns the string to display both rows for hours in the <see cref="BerlinClock"/> format
        /// </summary>
        /// <returns>
        /// A string representing the two rows which makes the hours
        /// </returns>
        private string DisplayHours()
        {
            const int HoursPerBlock = 5;
            const int MaximumLightPerRow = 4;
            
            // Build the first row. For each block of 5 hours, display a red light
            var consumedHourBlocksCount = Math.DivRem(this.timeToConvert.Hour, HoursPerBlock, out var remainingConsumedHoursCount);

            // remaining lights on that row must be switched off
            var emptyBlocksCount = MaximumLightPerRow - consumedHourBlocksCount;

            var topRow = string.Concat(
                ConvertToLight(LightRed, consumedHourBlocksCount),
                ConvertToLight(LightOff, emptyBlocksCount));

            // Then build the second row. for each hour not displayed withing the 5 hour blocks, display a red light.
            var remainingHoursCount = MaximumLightPerRow - remainingConsumedHoursCount;

            var bottomRow = string.Concat(
                ConvertToLight(LightRed, remainingConsumedHoursCount),
                ConvertToLight(LightOff, remainingHoursCount));

            return string.Concat(topRow, Environment.NewLine, bottomRow);
        }

        /// <summary>
        /// Returns the string to display both rows for minutes in the <see cref="BerlinClock"/> format
        /// </summary>
        /// <returns>
        /// A string representing the two rows which makes the minutes
        /// </returns>
        private string DisplayMinutes()
        {
            const int MaximumLightsOnTopRow = 11;
            const int MaximumLightsOnBottomRow = 4;
            const int MinutesPerBlock = 5;
            const int FifteenMinuteBlockIndex = 3;

            // Build the first row. For each block of 5 minutes, display a light.
            var consumedMinutesBlocksCount = Math.DivRem(this.timeToConvert.Minute, MinutesPerBlock, out var remainingConsumedMinutesCount);

            var lightBuilder = new StringBuilder();

            // Build the first row, made of 11 lights
            for (var i = 0; i < consumedMinutesBlocksCount; i++)
            {
                // Each 5 minutes is displayed as a yellow light, except  15 minute marks, displayed as red
                lightBuilder.Append(
                    (i + 1) % FifteenMinuteBlockIndex == 0 ?
                        ConvertToLight(LightRed, 1) :
                        ConvertToLight(LightYellow, 1));
            }

            // Add remaining switched off lights
            lightBuilder.Append(ConvertToLight(LightOff, MaximumLightsOnTopRow - consumedMinutesBlocksCount));

            // build second row
            lightBuilder.Append(Environment.NewLine);

            // remaining minutes that are not in a block must be yellow
            lightBuilder.Append(ConvertToLight(LightYellow, remainingConsumedMinutesCount));

            // complete row with switched off lights
            lightBuilder.Append(ConvertToLight(LightOff, MaximumLightsOnBottomRow - remainingConsumedMinutesCount));

            return lightBuilder.ToString();
        }

        /// <summary>
        /// Repeats a given light N times into a single string
        /// </summary>
        /// <param name="lightSymbol">
        /// The light symbol.
        /// </param>
        /// <param name="lightRepeatCount">
        /// The number of time to repeat the light.
        /// </param>
        /// <returns>
        /// The <see cref="string"/> showing a light in <see cref="BerlinClock"/> format, repeated N times
        /// </returns>
        private static string ConvertToLight(char lightSymbol, int lightRepeatCount)
        {
            return new string(lightSymbol, lightRepeatCount);
        }
    }
}
