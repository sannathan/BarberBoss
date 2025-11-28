using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;

namespace BarberBoss.Domain.Extensions
{
    public static class PaymentMethodExtension
    {
        public static string PaymentMethodToString(this PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.CreditCard => ResourceReportGenerationMessages.CREDIT_CARD,
                PaymentMethod.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
                PaymentMethod.Money => ResourceReportGenerationMessages.DEBIT_CARD,
                PaymentMethod.Pix => ResourceReportGenerationMessages.PIX,
                _ => string.Empty

            };
        }
    }
}
