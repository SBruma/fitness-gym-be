using FitnessGym.Domain.Entities.Enums;
using FitnessGym.Domain.Entities.Gyms;
using System.Linq.Expressions;
using Utils.Shared;

namespace FitnessGym.Domain.Filters
{
    public class EquipmentFilter
    {
        public EquipmentCategory? Category { get; set; } = null;
        public EquipmentStatus? Status { get; set; } = null;
        public DateOnly? PurchaseDate { get; set; } = null;
        public DateOnly? WarrantyExpirationDate { get; set; } = null;

        public Expression<Func<Equipment, bool>> GetQuery()
        {
            Expression<Func<Equipment, bool>> query = equipments => true;

            if (Category != null)
            {
                query = query.And(equipment => equipment.Category == Category.Value);
            }

            if (Status != null)
            {
                query = query.And(equipment => equipment.Status == Status.Value);
            }

            if (PurchaseDate != null)
            {
                query = query.And(equipment => equipment.PurchaseDate == PurchaseDate.Value);
            }

            if (WarrantyExpirationDate != null)
            {
                query = query.And(equipment => equipment.WarrantyExpirationDate == WarrantyExpirationDate.Value);
            }

            return query;
        }
    }
}
