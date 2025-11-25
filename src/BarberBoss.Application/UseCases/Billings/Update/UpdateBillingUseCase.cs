using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsB;

namespace BarberBoss.Application.UseCases.Billings.Update
{
    public class UpdateBillingUseCase : IUpdateBillingUseCase
    {
        private readonly IBillingsUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBillingUseCase(IBillingsUpdateOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(Guid Id, RequestBillingJson request)
        {
            Validate(request);

            var billing = await _repository.GetById(Id);

            if(billing is null)
            {
                throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
            }

            _mapper.Map(request, billing);

            billing.UpdatedAt = DateTime.UtcNow;

            _repository.Update(billing);

             await _unitOfWork.Commit();
        }

        private void Validate(RequestBillingJson request)
        {
            var validator = new BillingValidator();

            var result = validator.Validate(request);

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
