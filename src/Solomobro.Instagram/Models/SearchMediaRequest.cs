namespace Solomobro.Instagram.Models
{
    public class SearchMediaRequest
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int DistanceMeters { get; set; }
    }
}
