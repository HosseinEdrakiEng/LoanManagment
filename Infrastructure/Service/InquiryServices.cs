using Application.Abstraction;
using Application.Common;
using Application.Model;
using Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Service;

public class InquiryServices : IInquiryServices
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<InquiryServices> _logger;
    private readonly InqueryConfig _config;

    public InquiryServices(IHttpClientFactory httpClientFactory,
        ILogger<InquiryServices> logger,
        IOptions<InqueryConfig> config)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _config = config.Value;
    }

    //public async Task<BaseResponse<RaitingResponseModel>> Raiting(string nationalCode, CancellationToken cancellationToken)
    //{
    //    var response = new BaseResponse<RaitingResponseModel>();

    //    var headers = new Dictionary<string, string>
    //        {
    //            { "Content-Type", "application/json" }
    //        };
    //    var apiResponse = await _httpClientFactory.ApiCall("Inquiry", new object(), HttpMethod.Get, $"{_config.RatingUrl}/{nationalCode}", headers, cancellationToken);

    //    _logger.LogInformation($"Raiting log : '{apiResponse.SerializeAsJson()}'");

    //    if (!apiResponse.IsSuccessStatusCode)
    //    {
    //        // response.Error = CustomError.BalanceLoanFail;
    //        return response;
    //    }

    //    response.Data = JsonSerializer.Deserialize<RaitingResponseModel>(apiResponse.Response);
    //    return response;
    //}
}
