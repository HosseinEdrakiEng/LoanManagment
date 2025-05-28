using System;
using System.Collections.Generic;

namespace Domain.Entites;

public partial class NotRegisterationCreditPlanRequest
{
    public long Id { get; set; }

    public DateTime CreateTime { get; set; } = DateTime.Now;

    public string UserId { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string NationalCode { get; set; } = null!;

    public byte? ConfigTypeId { get; set; }

    public long CurrencyId { get; set; }

    public byte Status { get; set; } = 0;

    public string ClientId { get; set; } = null!;

    public long GroupId { get; set; }

    public string Level { get; set; } = null!;
}
