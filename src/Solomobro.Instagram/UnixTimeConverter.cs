using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;

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
    }
}
