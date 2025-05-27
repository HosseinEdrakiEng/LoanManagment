using Application.Abstraction.IRepository;
using Application.Abstraction.IService;
using Application.Model;
using Domain.Entites;
using Helper;
using Mapster;

namespace Infrastructure.Service
{
    public class CreditPlanRequestService : ICreditPlanRequestService
    {
        private readonly INotRegisterationCreditPlanRequestRepository _notRegisterationCreditPlanRequestRepository;

        public CreditPlanRequestService(INotRegisterationCreditPlanRequestRepository notRegisterationCreditPlanRequestRepository)
        {
            _notRegisterationCreditPlanRequestRepository = notRegisterationCreditPlanRequestRepository;
        }

        public async Task<BaseResponse<PreRegsitrationResponseModel>> PreRegsitration(PreRegsitrationRequestModel request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<PreRegsitrationResponseModel>();

            var models = new List<NotRegisterationCreditPlanRequest>();

            foreach (var item in request.Items)
            {
                var model = request.Adapt<NotRegisterationCreditPlanRequest>();
                model.CurrencyId = item.CurrencyId;
                model.ConfigTypeId = item.ConfigTypeId;
                models.Add(model);
            }

            await _notRegisterationCreditPlanRequestRepository.BulkInsert(models, cancellationToken);
            return response;
        }
    }
}
