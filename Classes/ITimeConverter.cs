namespace BerlinClock
{
    /// <summary>
    /// A time converter used to interact with specific clocks
    /// </summary>
    public interface ITimeConverter
    {
        /// <summary>
        /// Converts a given time into a given clock's time
        /// </summary>
        /// <param name="timeToRead">Time in format HH:mm:ss Range must be between 00:00:00 and 23:59:59</param>
        /// <returns>The time as read in the given clock</returns>
        string ConvertTime(string timeToRead);
    }
}
