namespace Solomobro.Instagram.Models
{
    // todo: consider making this a struct instead
    public class RateLimit
    {
        internal RateLimit() { }
        public int Max { get; internal set; }
        public int Remaining { get; internal set; }
    }
}
