using Application.Abstraction.IService;
using Application.Model;
using Asp.Versioning;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LoanManagment.Api.Controller.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class CreditPlanController : ControllerBase
    {
        private readonly ICreditPlanService _creditPlanService;

        public CreditPlanController(ICreditPlanService creditPlanService)
        {
            _creditPlanService = creditPlanService;
        }

        [HttpGet("load")]
        public async Task<IActionResult> Load([FromRoute, Required] long groupId, CancellationToken cancellationToken)
        {
            var dto = new LoadCreditPlanRequestModel
            {
                UserId = Request.HttpContext.GetUserId(),
                ClientId = Request.HttpContext.GetClientId(),
                GroupId = groupId,
                PhoneNumber = Request.HttpContext.GetUserPhonenumber(),
            };
            var response = await _creditPlanService.LoadCreditPlan(dto, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }
    }
}
