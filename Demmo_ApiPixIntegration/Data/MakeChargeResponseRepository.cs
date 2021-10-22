using Demmo_ApiPixIntegration.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demmo_ApiPixIntegration.Data

{
    public class MakeChargeResponseRepository : IMakeChargeResponseRepository
    {
        private readonly IMongoCollection<InstantChargeResponse> _makeChargeResponseCollection = null;

        /// <summary>
        /// Cria uma nova instância de WhatsappResponseRepository
        /// </summary>
        public MakeChargeResponseRepository(IMongoDatabase database)
        {
            _makeChargeResponseCollection = database.GetCollection<InstantChargeResponse>("MakeChargeResponse");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public async Task SaveAsync(InstantChargeResponse response)
        {
            await _makeChargeResponseCollection.ReplaceOneAsync(r => r.Id.Equals(response.Id), response, new ReplaceOptions { IsUpsert = true });
        }
    }
}
