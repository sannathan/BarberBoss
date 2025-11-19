namespace BarberBoss.Communication.Responses
{
    public class ResponseRegisterBillingJson
    {
        public  DateOnly Date { get; set; }
        public  string BarberName { get; set; } = string.Empty;
        public  string ClientName { get; set; } = string.Empty;
        public  string ServiceName { get; set; } = string.Empty;
    }
}
