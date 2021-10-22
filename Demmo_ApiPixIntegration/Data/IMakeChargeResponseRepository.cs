using Demmo_ApiPixIntegration.Models;
using System.Threading.Tasks;

namespace Demmo_ApiPixIntegration.Data
{
    public interface IMakeChargeResponseRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task SaveAsync(InstantChargeResponse request);
    }
}
