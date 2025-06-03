using Application.Abstraction;
using Application.Common;
using Application.Model;
using Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Service;

public class WalletServices : IWalletServices
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WalletServices> _logger;
    private readonly WalletConfig _config;

    public WalletServices(IHttpClientFactory httpClientFactory,
        ILogger<WalletServices> logger,
        IOptions<WalletConfig> config)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _config = config.Value;
    }

    public async Task<BaseResponse<CreateWalletResponseModel>> CreateWallet(CreateWalletRequestModel request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CreateWalletResponseModel>();

        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var apiResponse = await _httpClientFactory.ApiCall("Wallet", request, HttpMethod.Post, _config.CreateUrl, headers, cancellationToken);

        _logger.LogInformation($"CreateWallet log : '{apiResponse.SerializeAsJson()}'");

        if (!apiResponse.IsSuccessStatusCode
            || string.IsNullOrWhiteSpace(apiResponse.Response))
        {
            response.Error = CustomErrors.CreateWalletError;
            return response;
        }

        response.Data = JsonSerializer.Deserialize<CreateWalletResponseModel>(apiResponse.Response);
        return response;
    }
    public async Task<BaseResponse<ChargeResponseModel>> Charge(ChargeRequestModel request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ChargeResponseModel>();

        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };

        var apiResponse = await _httpClientFactory.ApiCall("Wallet", request, HttpMethod.Post, _config.ChargeUrl, headers, cancellationToken);

        _logger.LogInformation($"Charge Wallet log : '{apiResponse.SerializeAsJson()}'");

        if (!apiResponse.IsSuccessStatusCode
            || string.IsNullOrWhiteSpace(apiResponse.Response))
        {
            response.Error = CustomErrors.ChargeWalletError;
            return response;
        }

        response.Data = JsonSerializer.Deserialize<ChargeResponseModel>(apiResponse.Response);
        return response;
    }
    public async Task<BaseResponse<AdviceResponseModel>> Advice(AdviceRequestModel request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AdviceResponseModel>();

        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };

        var apiResponse = await _httpClientFactory.ApiCall("Wallet", request, HttpMethod.Post, _config.AdviceUrl, headers, cancellationToken);

        _logger.LogInformation($"Advice log : '{apiResponse.SerializeAsJson()}'");

        if (!apiResponse.IsSuccessStatusCode
            || string.IsNullOrWhiteSpace(apiResponse.Response))
        {
            response.Error = CustomErrors.AdviceWalletError;
            return response;
        }

        return response;
    }
    public async Task<BaseResponse<ReverseResponseModel>> Reverse(ReverseRequestModel request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ReverseResponseModel>();

        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };

        var apiResponse = await _httpClientFactory.ApiCall("Wallet", request, HttpMethod.Post, _config.ReverseUrl, headers, cancellationToken);

        _logger.LogInformation($"Reverse log : '{apiResponse.SerializeAsJson()}'");

        if (!apiResponse.IsSuccessStatusCode
            || string.IsNullOrWhiteSpace(apiResponse.Response))
        {
            response.Error = CustomErrors.ReverseWalletError;
            return response;
        }

        return response;
    }
}