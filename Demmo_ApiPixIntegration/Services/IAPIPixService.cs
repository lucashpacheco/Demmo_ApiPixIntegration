using Demmo_ApiPixIntegration.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demmo_ApiPixIntegration.Services
{
    public interface IAPIPixService
    {
        Task<List<InstantChargeResponse>> MakeCharge(List<MakeChargeRequest> requests);
    }
}
