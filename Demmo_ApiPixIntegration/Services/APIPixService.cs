using Demmo_ApiPixIntegration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demmo_ApiPixIntegration.Services
{
    public class APIPixService : IAPIPixService
    {
        private readonly IConfiguration _configuration;
        private readonly APIPixSettings _aPIPixSettings;

        public APIPixService(IConfiguration configuration , IOptions<APIPixSettings> options)
        {
            _configuration = configuration;
            _aPIPixSettings = options.Value;
        }
        /// <summary>
        /// Chama o serviço responsavel pelo envio.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<List<InstantChargeResponse>> MakeCharge(List<MakeChargeRequest> message)
        {
            var engine = APIPixFactory.CreateAPIPixService(message[0].CampaignId , _configuration , _aPIPixSettings);

            return await engine.MakeCharge(message);
        }
    }
}
