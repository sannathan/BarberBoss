using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using Microsoft.IdentityModel.Tokens;

namespace BarberBoss.Application.UseCases.Billings.Register
{
    public class RegisterBillingUseCase : IRegisterBillingUseCase
    {
        private readonly IBillingsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterBillingUseCase(IBillingsWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReponseRegisterBillingJson> IRegisterBillingUseCase.Execute(RequestBillingJson request)
        {
            Validate(request);

             

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
