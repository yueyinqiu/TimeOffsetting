using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOffsetting
{
    /// <summary>
    /// A time offsets provider.
    /// </summary>
    public static class TimeOffsets
    {
        /// <summary>
        /// Get the offset by a given longitude.
        /// This will returns the real local time offset rather than the local time zones' offset.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <returns>The offset.</returns>
        public static TimeSpan GetByLongitude(long longitude)
        {
            var ticks = longitude * 4 * 60 * 10000000;
            return new TimeSpan(ticks);
        }

        /// <summary>
        /// Get the offset by a given longitude.
        /// This will returns the real local time offset rather than the local time zones' offset.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <returns>The offset.</returns>
        public static TimeSpan GetByLongitude(double longitude)
        {
            var ticks = longitude * 4 * 60 * 10000000;
            return new TimeSpan((long)ticks);
        }

        /// <summary>
        /// Get the time offset of the utc time.
        /// This will always return <see cref="TimeSpan.Zero"/>.
        /// </summary>
        public static TimeSpan Utc => TimeSpan.Zero;

        /// <summary>
        /// Get the time offset of the local time zone.
        /// </summary>
        public static TimeSpan LocalZone => TimeZoneInfo.Local.BaseUtcOffset;

        /// <summary>
        /// Get the time offset of the given time zone.
        /// </summary>
        /// <param name="systemTimeZoneId">The id of the time zone.</param>
        /// <returns>The offset.</returns>
        /// <exception cref="OutOfMemoryException">
        /// The system does not have enough memory to hold information about the time zone.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="systemTimeZoneId"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="TimeZoneNotFoundException">
        /// The time zone identifier specified by id was not found. This means that a time zone identifier whose name matches id does not exist, or that the identifier exists but does not contain any time zone data.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// The process does not have the permissions required to read from the registry key that contains the time zone information.
        /// </exception>
        /// <exception cref="InvalidTimeZoneException">
        /// The time zone identifier was found, but the registry data is corrupted.
        /// </exception>
        public static TimeSpan GetByTimeZone(string systemTimeZoneId)
        {
            if (systemTimeZoneId is null)
                throw new ArgumentNullException(nameof(systemTimeZoneId));
            return TimeZoneInfo.FindSystemTimeZoneById(systemTimeZoneId).BaseUtcOffset;
        }

        /// <summary>
        /// Get the time offset of the given time zone.
        /// </summary>
        /// <param name="timeZoneInfo">The time zone.</param>
        /// <returns>The offset.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="timeZoneInfo"/> is <c>null</c>.
        /// </exception>
        public static TimeSpan GetByTimeZone(TimeZoneInfo timeZoneInfo)
        {
            if (timeZoneInfo is null)
                throw new ArgumentNullException(nameof(timeZoneInfo));
            return timeZoneInfo.BaseUtcOffset;
        }
    }
}
