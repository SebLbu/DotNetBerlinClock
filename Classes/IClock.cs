namespace BerlinClock.Classes
{
    /// <summary>
    /// The Clock interface.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Display's the time in <see cref="IClock"/> format
        /// </summary>
        /// <returns>
        /// A string representing the time as displayed on the clock
        /// </returns>
        string ToString();
    }
}