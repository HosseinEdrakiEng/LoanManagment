﻿namespace Application.Common;

public class WalletConfig
{
    public string BaseUrl { get; set; }
    public TimeSpan Timeout { get; set; }
    public string ChargeUrl { get; set; }
    public string CreateUrl { get; set; }
    public string AdviceUrl { get; set; }
    public string ReverseUrl { get; set; }
}
