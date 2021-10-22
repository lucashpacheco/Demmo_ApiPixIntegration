using Demmo_ApiPixIntegration.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demmo_ApiPixIntegration.Data

{
    public class MakeChargeRequestRepository : IMakeChargeRequestRepository
    {
        private readonly IMongoCollection<MakeChargeRequest> _makeChargeRequestCollection = null;

        /// <summary>
        /// Cria uma nova instância de MakeChargeRequestRepository
        /// </summary>
        public MakeChargeRequestRepository(IMongoDatabase database)
        {
            _makeChargeRequestCollection = database.GetCollection<MakeChargeRequest>("MakeChargeRequest");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SaveAsync(MakeChargeRequest request)
        {
            await _makeChargeRequestCollection.ReplaceOneAsync(r => r.Id.Equals(request.Id), request, new ReplaceOptions { IsUpsert = true });
        }
    }
}
