namespace FitnessGym.Application.Errors.Gyms
{
    public class EquipmentNotCreatedError : NotCreatedError
    {
        public EquipmentNotCreatedError() : base("Equipment couldn't be created")
        {
        }
    }
}
