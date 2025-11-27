
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsB;

namespace BarberBoss.Application.UseCases.Billings.Delete
{
    public class DeleteBillingUseCase : IDeleteBillingUseCase
    {
        private readonly IBillingsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBillingUseCase(IBillingsWriteOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(Guid Id)
        {
            var result = await _repository.Delete(Id);

            if(result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
            }

            await _unitOfWork.Commit(); 
        }
    }
}
