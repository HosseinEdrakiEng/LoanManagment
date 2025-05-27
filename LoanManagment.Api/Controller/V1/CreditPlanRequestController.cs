using Application.Abstraction.IService;
using Application.Model;
using Asp.Versioning;
using Helper;
using LoanManagment.Api.Model;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagment.Api.Controller.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CreditPlanRequestController : ControllerBase
    {
        private readonly ICreditPlanRequestService _creditPlanRequestService;

        public CreditPlanRequestController(ICreditPlanRequestService creditPlanRequestService)
        {
            _creditPlanRequestService = creditPlanRequestService;
        }

        [HttpPost("PreRegsitration")]
        [Authorize]
        public async Task<IActionResult> PreRegsitration([FromBody] PreRegsitrationRequestDto request, CancellationToken cancellationToken)
        {
            var model = request.Adapt<PreRegsitrationRequestModel>();
            model.UserId = HttpContext.GetUserId();
            model.ClientId = HttpContext.GetClientId();
            var response = await _creditPlanRequestService.PreRegsitration(model, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }
    }
}
