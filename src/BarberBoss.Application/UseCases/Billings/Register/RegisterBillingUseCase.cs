using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Billings.Register
{
    public class RegisterBillingUseCase : IRegisterBillingUseCase
    {
        Task<ReponseRegisterBillingJson> IRegisterBillingUseCase.Execute(RequestBillingJson request)
        {
        }
    }
}
