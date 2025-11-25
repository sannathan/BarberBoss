using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Billings.Update
{
    public interface IUpdateBillingUseCase
    {
        Task Execute(Guid Id, RequestBillingJson request);
    }
}
