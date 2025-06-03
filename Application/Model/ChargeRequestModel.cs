namespace Application.Model;

public record ChargeRequestModel(int ConfigType, int Currency, string PhoneNumber, long GroupId, long Amount, string ClientRefNo);
public record ChargeResponseModel(string TrackingCode, string ClientRefNo);
