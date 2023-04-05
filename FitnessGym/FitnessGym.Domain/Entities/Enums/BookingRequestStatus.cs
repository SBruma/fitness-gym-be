namespace FitnessGym.Domain.Entities.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum BookingRequestStatus
    {
        PENDING,
        APPROVED,
        DECLINED
    }
}
