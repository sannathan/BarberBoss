using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper
{
    internal class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestBillingJson, Billing>();
        }

        private void EntityToResponse()
        {
            CreateMap<Billing, ReponseRegisterBillingJson>();
            CreateMap<Billing, ResponseShortBillingJson>();
            CreateMap<Billing, ResponseBillingJson>();
        }
    }
}
