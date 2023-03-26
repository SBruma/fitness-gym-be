namespace FitnessGym.Domain.Entities.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
