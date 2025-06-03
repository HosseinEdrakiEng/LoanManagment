namespace Application.Model;

public record CreateWalletRequestModel(int ConfigType, long Currency, string PhoneNumber, long GroupId);

public record CreateWalletResponseModel(string WalletId);

