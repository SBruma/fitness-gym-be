using FitnessGym.Domain.Entities.Gyms;
using System.Linq.Expressions;
using Utils.Shared;

namespace FitnessGym.Domain.Filters
{
    public class EquipmentMaintenanceFilter
    {
        public Guid? EquipmentId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool? Active { get; set; }

        public Expression<Func<MaintenanceHistory, bool>> GetFilterQuery()
        {
            Expression<Func<MaintenanceHistory, bool>> query = maintenanceHistory => true;

            if (EquipmentId is not null)
            {
                query = query.And(maintenanceHistory => maintenanceHistory.EquipmentId == new EquipmentId(EquipmentId.Value));
            }

            if (StartDate is not null)
            {
                query = query.And(maintenanceHistory => maintenanceHistory.StartDate == StartDate);
            }

            if (Active is not null)
            {
                query = Active.Value ? query.And(maintenanceHistory => maintenanceHistory.EndDate == null) :
                    query.And(maintenanceHistory => maintenanceHistory.EndDate != null);
            }

            if (EndDate is not null)
            {
                query = query.And(maintenanceHistory => maintenanceHistory.EndDate == EndDate);
            }

            return query;
        }
    }
}
