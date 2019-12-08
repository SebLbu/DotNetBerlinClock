using System;

namespace BerlinClock
{
    /// <summary>
    /// The Clock interface.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Display's the time in <see cref="IClock"/> format
        /// </summary>
        /// <param name="time">
        /// The time to read in <see cref="IClock"/>'s format
        /// </param>
        /// <returns>
        /// A string representing the time as displayed on the clock
        /// </returns>
        string ReadTime(DateTime time);
    }
}