using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
