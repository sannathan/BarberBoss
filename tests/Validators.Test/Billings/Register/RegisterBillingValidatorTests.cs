using BarberBoss.Application.UseCases.Billings;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception;
using CommonTestUtilities.Requests;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using FluentAssertions;
using Xunit;

namespace Validators.Test.Billings.Register
{
    public class RegisterBillingValidatorTests
    {
        [Fact]
        public void Success()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public void Error_ServiceName_Empty(string serviceName)
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.ServiceName = serviceName;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.SERVICE_NAME_REQUIRED));
        }

        [Fact]
        public void Error_Date_Future()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DATE_CANNOT_BE_FUTURE));
        }

        [Fact]
        public void Error_Payment_Type_Not_Valid()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.PaymentMethod = (PaymentMethod)100;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_METHOD_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100000000000000)]
        [InlineData(-1000000000000007)]
        public void Error_Amount_Invalid(decimal amount)
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.Amount = amount;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
        }
    }
}
