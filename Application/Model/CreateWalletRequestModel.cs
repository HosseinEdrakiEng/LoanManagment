using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model;

public record CreateWalletRequestModel(int ConfigType, long Currency, string PhoneNumber, string TerminalId, long GroupId);

public record CreateWalletResponseModel(string WalletId);

