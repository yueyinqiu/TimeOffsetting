using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOffsetting
{
    /// <summary>
    /// Represents a utc date time.
    /// It's just a media for conversions and transmissions,
    /// and should be convert back to <see cref="DateTime"/> before being used.
    /// </summary>
    public class UtcDateTime
    {
        private DateTime dateTime;

        /// <summary>
        /// Initialize an instance of <seealso cref="UtcDateTime"/>.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public UtcDateTime(DateTimeOffset dateTime)
        {
            this.dateTime = new DateTime(dateTime.UtcTicks, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Initialize an instance of <seealso cref="UtcDateTime"/>.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="offset">
        /// The offset of the given date time.
        /// This will be used to convert the given date time to a utc one.
        /// If set to <c>null</c>, <see cref="DateTime.ToUniversalTime"/> will be used to convert it.
        /// </param>
        public UtcDateTime(DateTime dateTime, TimeSpan? offset = null)
        {
            if (offset.HasValue)
                this.dateTime = DateTime.SpecifyKind(
                    dateTime - offset.Value, DateTimeKind.Unspecified);
            else
                this.dateTime = DateTime.SpecifyKind(
                    dateTime.ToUniversalTime(), DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Get an instance of <see cref="DateTime"/> by the given offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public DateTime GetDateTime(TimeSpan offset)
        {
            return dateTime + offset;
        }
    }
}
