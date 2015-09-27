using System;

namespace Solomobro.Instagram
{
    internal class UnixTimeConverter
    {
        private static DateTime _nineteenSeventy = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ConvertFromUnixTime(long unixTime)
        {
            return _nineteenSeventy.AddSeconds(unixTime);
        }

        public static DateTime ConvertFromUnixTime(string unixTime)
        {
            return ConvertFromUnixTime(long.Parse(unixTime));
        }

        public static long ConvertToUnixTime(DateTime dt)
        {
            return Convert.ToInt64((dt - _nineteenSeventy).TotalSeconds);
        }
    }
}
