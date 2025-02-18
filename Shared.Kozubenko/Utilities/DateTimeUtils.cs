using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kozubenko.Utilities
{
    public static class DateTimeUtils
    {
        /// <summary>
        /// Unix time in seconds since 1970.
        /// </summary>
        public static uint UnixTimestamp
        {
            get { return ToUnixTimestamp(DateTime.UtcNow); }
        }

        /// <summary>
        /// Converts DateTime to seconds since 1970.
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static uint ToUnixTimestamp(DateTime? now = null)
        {
            if (now == null)
                now = DateTime.UtcNow;

            TimeSpan t = (now.Value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (uint)t.TotalSeconds;
        }

        public static DateTime ToUnixDateTime(uint SecondsSinceJan1970 = 0)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(SecondsSinceJan1970);
        }
    }
}
