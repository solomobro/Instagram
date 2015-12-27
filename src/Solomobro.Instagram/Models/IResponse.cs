namespace Solomobro.Instagram.Models
{
    internal interface IResponse
    {

         Meta Meta { get; }

        RateLimit RateLimit { get; }
    }
}
