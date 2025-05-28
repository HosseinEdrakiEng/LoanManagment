using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service;

internal class Class1
{
    public async Task ProcessNotRegisterationCreditPlanAsync(CancellationToken cancellationToken)
    {
        var notRegisterationCreditPlanRequests = await _notRegisterationCreditPlanRequestRepository.GetByNoneStatus(cancellationToken);

        List<long> notRegisterationCreditPlans = new List<long>();

        foreach (var item in notRegisterationCreditPlanRequests)
        {
            var ratingResponse = await _inqueryServices.Raiting(item.NationalCode, cancellationToken);
            if (ratingResponse.HasError)
            {
                _logger.LogWarning($"Error in request rating for NationalCode: {item.NationalCode}. Error: {ratingResponse.Error}");
                continue;
            }

            var walletResponse = await _walletServices.CreateWallet(new CreateWalletRequestModel((int)item.ConfigTypeId, item.CurrencyId, item.PhoneNumber, item.ClientId, item.GroupId), cancellationToken);

            if (walletResponse.HasError || string.IsNullOrEmpty(walletResponse.Data.ToString()))
            {
                var text = walletResponse.HasError ? $"Error: {ratingResponse.Error}" : $" data is empty";
                _logger.LogWarning($"Error in Create Wallet for NationalCode: {item.NationalCode}. {text}");
                continue;
            }

            var creditLimit = await _creditLimitRepository.GetQueryable()
                    .Where(a => a.ConfigTypeId == item.ConfigTypeId)
                    .Where(a => a.CreditRate.Range == ratingResponse.Data.ScoreCode)
                    // .Where(a=>a.CreditRate.Range == (int)ratingResponse.Data.Score)
                    .Where(a => a.CreditLevel.Title == item.Level)
                    .Where(a => a.CurrencyId == item.CurrencyId)
                    .FirstOrDefaultAsync(cancellationToken);

            if (creditLimit is null)
            {
                _logger.LogWarning($"Error in creditLimit for NationalCode: {item.NationalCode} is null ");
                continue;
            }

            var clientRefNo = Extention.GenerateRandomCode();

            var chargeResponse = await _walletServices.Charge(new ChargeRequestModel((int)item.ConfigTypeId, (int)item.CurrencyId, item.PhoneNumber, item.GroupId, creditLimit.Limit, clientRefNo), cancellationToken);

            if (chargeResponse.HasError || chargeResponse.Data is null)
            {
                var text = chargeResponse.HasError ? $"Error: {chargeResponse.Error}" : $" data is empty";
                _logger.LogWarning($"Error in Create Wallet for NationalCode: {item.NationalCode}. {text}");

                var reverseResponse = await _walletServices.Reverse(new ReverseRequestModel(null, clientRefNo), cancellationToken);

                if (reverseResponse.HasError)
                    _logger.LogWarning($"Error in Reverse for NationalCode: {item.NationalCode}. Error: {reverseResponse.Error}");

                continue;
            }


            var adviceResponse = await _walletServices.Advice(new AdviceRequestModel(chargeResponse.Data.TrackingCode), cancellationToken);
            if (adviceResponse.HasError)
            {
                _logger.LogWarning($"Error in Advice for NationalCode: {item.NationalCode}. Error: {adviceResponse.Error}");
                continue;
            }
            var creditPlan = await _creditPlanRepository.GetByFilter(new CreditPlanFilter()
            {
                ConfigTypeId = item.ConfigTypeId,
                CurrencyId = item.CurrencyId,
                Enable = true
            }, cancellationToken);

            await _creditPlanRequestRepository.Insert(new CreditPlanRequest()
            {
                UserId = item.UserId,
                Status = (byte)CreditPlanType.None,
                CreditPlanId = creditPlan.Id,
            }, cancellationToken);

            notRegisterationCreditPlans.Add(item.Id);
        }

        if (notRegisterationCreditPlans.Count > 0)
            await _notRegisterationCreditPlanRequestRepository.UpdateStatus(notRegisterationCreditPlans, NotRegisterationStatusType.Done, cancellationToken);
    }
}
