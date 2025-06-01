using Application.Abstraction;
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
    public class CreditPlanRequestController : ControllerBase
    {
       
    }
}
