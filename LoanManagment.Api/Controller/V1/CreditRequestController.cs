﻿using Application.Abstraction;
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
    [Authorize]
    public class CreditRequestController : ControllerBase
    {
        private readonly ICreditRequestServices _creditRequestServices;

        public CreditRequestController(ICreditRequestServices creditRequestServices)
        {
            _creditRequestServices = creditRequestServices;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCreditRequest request, CancellationToken cancellationToken)
        {
            var model = request.Adapt<CreateCerditRequestModel>();
            model.UserId = HttpContext.GetUserId();
            model.NationalCode = HttpContext.GetUserNationalCode();

            var response = await _creditRequestServices.CreateCreditRequest(model, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }
    }
}
