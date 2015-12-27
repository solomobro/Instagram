using System;

namespace Solomobro.Instagram.Models
{
    public class MediaSearchOptions
    {
        public string MinId { get; set; }
        public string MaxId { get; set; }
        public DateTime? MinTimestamp { get; set; }
        public DateTime? MaxTimestamp { get; set; }
    }
}
