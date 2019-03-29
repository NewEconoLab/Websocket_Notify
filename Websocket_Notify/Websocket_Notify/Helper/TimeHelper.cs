using System;

namespace Websocket_Notify
{
    public class TimeHelper
    {
        public static long GetTimeStamp()
        {
            return toTimeStamp(DateTime.UtcNow);
        }
        public static long toTimeStamp(DateTime tm)
        {
            TimeSpan ts = tm - ZERO_DATETIME;
            return Convert.ToInt64(ts.TotalSeconds);
        }
        private static DateTime ZERO_DATETIME = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    }
}
