using Application.Abstraction;
using Application.Common;
using Application.Model;
using Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Service;

public class UserServices : IUserServices
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<UserServices> _logger;
    private readonly UserConfig _config;

    public UserServices(IHttpClientFactory httpClientFactory,
        ILogger<UserServices> logger,
        IOptions<UserConfig> config)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _config = config.Value;
    }

    public async Task<BaseResponse<UserProfileResponseModel>> Profile(string mobileNumber, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserProfileResponseModel>();

        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };

        var apiResponse = await _httpClientFactory.ApiCall("User", new object(), HttpMethod.Get, $"{_config.ProfileUrl}/{mobileNumber}", headers, cancellationToken);

        _logger.LogInformation($"Profile log : '{apiResponse.SerializeAsJson()}'");

        if (!apiResponse.IsSuccessStatusCode)
        {
            response.Error = CustomErrors.UserProfileError;
            return response;
        }

        response.Data = JsonSerializer.Deserialize<UserProfileResponseModel>(apiResponse.Response);
        return response;
    }

}
