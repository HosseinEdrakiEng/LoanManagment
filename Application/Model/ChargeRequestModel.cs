using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model;

public record ChargeRequestModel(int ConfigType,int Currency,string PhoneNumber,long GroupId,long Amount,string ClientRefNo);
public record ChargeResponseModel(string TrackingCode, string ClientRefNo);
