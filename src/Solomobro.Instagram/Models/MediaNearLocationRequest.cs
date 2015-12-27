using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    public class MediaNearLocationRequest
    {
        public MediaNearLocationRequest(string mediaId)
        {
            MediaId = mediaId;
        }
        public string MediaId { get; private set; }
        public string MinId { get; set; }
        public string MaxId { get; set; }
        public DateTime MinTimestamp { get; set; }
        public DateTime MaxTimestam { get; set; }
    }
}
