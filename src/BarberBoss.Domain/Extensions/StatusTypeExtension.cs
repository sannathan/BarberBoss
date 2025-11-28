using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;
using System.Runtime.CompilerServices;

namespace BarberBoss.Domain.Extensions
{
    public static class StatusTypeExtension
    {
        public static string StatusTypeToString(this Status status)
        {
            return status switch { 
                Status.paid => ResourceReportGenerationMessages.PAID,
                Status.canceled => ResourceReportGenerationMessages.CANCELED, 
                _ => string.Empty 
            };
        }
    }
}
