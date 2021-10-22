using Demmo_ApiPixIntegration.Data;
using Demmo_ApiPixIntegration.Models;
using Demmo_ApiPixIntegration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demmo_ApiPixIntegration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIPixController : ControllerBase
    {
        private readonly IAPIPixService _apipixService;
        public IMakeChargeRequestRepository _makeChargeRequestRepository { get; private set; }
        public IMakeChargeResponseRepository _makeChargeResponseRepository { get; private set; }

    //private readonly IMakeChargeRequestRepository _makeChargeRequestRepository;
    //private readonly IMakeChargeResponseRepository _makeChargeResponseRepository; 

    public APIPixController(ILogger<APIPixController> logger , IAPIPixService aPIPixService , IMakeChargeRequestRepository makeChargeRequestRepository , IMakeChargeResponseRepository makeChargeResponseRepository)
        {
            _logger = logger;
            _apipixService = aPIPixService;
            _makeChargeRequestRepository = makeChargeRequestRepository;
            _makeChargeResponseRepository = makeChargeResponseRepository;
        }

        private readonly ILogger<APIPixController> _logger;

        
        [HttpPost]
        public async Task<IActionResult> MakeCharge([FromBody] List<MakeChargeRequest> requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelErrors(ModelState));
            }
            foreach (var request in requests)
            {
                await _makeChargeRequestRepository.SaveAsync(request);
            }
            var responses = await _apipixService.MakeCharge(requests);
            foreach (var response in responses)
            {
                await _makeChargeResponseRepository.SaveAsync(response);
            }
            return Ok(responses);
        }
    }
}
