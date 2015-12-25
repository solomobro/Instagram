using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    public class SearchMediaRequest
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int DistanceMeters { get; set; }
    }
}
