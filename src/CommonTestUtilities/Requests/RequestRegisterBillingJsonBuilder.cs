using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterBillingJsonBuilder
    {
        public static RequestBillingJson Build()
        {
            return new Faker<RequestBillingJson>()
                .RuleFor(r => r.Date, faker => DateOnly.FromDateTime(faker.Date.Recent(30)))
                .RuleFor(r => r.BarberName, faker => faker.Name.FirstName())
                .RuleFor(r => r.ClientName, faker => faker.Name.FirstName())
                .RuleFor(r => r.ServiceName, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Amount, faker => faker.Finance.Amount(min:1, max:100))
                .RuleFor(r => r.PaymentMethod, faker => faker.Random.Enum<PaymentMethod>())
                .RuleFor(r => r.Status, faker => faker.Random.Enum<Status>())
                .RuleFor(r => r.Notes, faker => faker.Commerce.ProductDescription());
        }
    }
}
