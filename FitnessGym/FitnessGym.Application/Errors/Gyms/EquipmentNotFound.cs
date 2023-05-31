using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Application.Errors.Gyms
{
    public class EquipmentNotFound : NotFoundError
    {
        public EquipmentNotFound(EquipmentId equipmentId)
            : base($"Equipment with id {equipmentId} couldn't be found")
        {

        }
    }
}
