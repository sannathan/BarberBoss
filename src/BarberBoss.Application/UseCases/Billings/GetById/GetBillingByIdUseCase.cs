using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsB;

namespace BarberBoss.Application.UseCases.Billings.GetById
{
    public class GetBillingByIdUseCase : IGetBillingByIdUseCase
    {
        private readonly IBillingsReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetBillingByIdUseCase(IBillingsReadOnlyRepository repository,IMapper mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseBillingJson> Execute(Guid Id)
        {
            var result = await _repository.GetById(Id);

            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
            }

            return _mapper.Map<ResponseBillingJson>(result);
        }
    }
}
