using AutoMapper.Configuration;
using BarberBoss.Communication.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Billings
{
    public class BillingValidator : AbstractValidator<RequestBillingJson>
    {
        public BillingValidator()
        {
            RuleFor(billing => billing.Date).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage(ResourceErrorMessages.DATE_CANNOT_BE_FUTURE);
            RuleFor(billing => billing.BarberName).MinimumLength(2).WithMessage(ResourceErrorMessages.BARBER_NAME_MUST_BE_GREATHER_THAN_TWO_LETTERS);
            RuleFor(billing => billing.BarberName).MaximumLength(80).WithMessage(ResourceErrorMessages.BARBER_NAME_MUST_BE_LESS_THAN_EIGHTY_LETTERS);
            RuleFor(billing => billing.ClientName).MinimumLength(2).WithMessage(ResourceErrorMessages.CLIENT_NAME_MUST_BE_GREATHER_THAN_TWO_LETTERS);
            RuleFor(billing => billing.ClientName).MaximumLength(120).WithMessage(ResourceErrorMessages.CLIENT_NAME_MUST_BE_LESS_THAN_ONE_HUNDRED_TWENTY_LETTERS);
            RuleFor(billing => billing.ServiceName).NotEmpty().WithMessage(ResourceErrorMessages.SERVICE_NAME_REQUIRED);
            RuleFor(billing => billing.ServiceName).MaximumLength(120).WithMessage(ResourceErrorMessages.SERVICE_NAME_MUST_BE_LESS_THAN_ONE_HUNDRED_TWENTY_LETTERS);
            RuleFor(billing => billing.Amount).GreaterThanOrEqualTo(1).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
            RuleFor(billing => billing.PaymentMethod).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_METHOD_INVALID);
            RuleFor(billing => billing.Status).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_METHOD_INVALID);
        }
    }
}
