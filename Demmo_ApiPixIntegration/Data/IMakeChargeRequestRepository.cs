using Demmo_ApiPixIntegration.Models;
using System.Threading.Tasks;

namespace Demmo_ApiPixIntegration.Data
{
    public interface IMakeChargeRequestRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task SaveAsync(MakeChargeRequest request);
    }
}
