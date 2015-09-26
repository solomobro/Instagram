using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Interfaces
{
    public interface IPagination<T> where T : Response
    {
        Task<T> GetNextResultAsync();
    }
}
