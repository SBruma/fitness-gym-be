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
        public Guid? GymId { get; set; } = null;
        public string? Name { get; set; } = null;

        public Expression<Func<Equipment, bool>> GetQuery()
        {
            Expression<Func<Equipment, bool>> query = equipments => true;

            if (Category is not null)
            {
                query = query.And(equipment => equipment.Category == Category);
            }

            if (Status is not null)
            {
                query = query.And(equipment => equipment.Status == Status);
            }

            if (PurchaseDate is not null)
            {
                query = query.And(equipment => equipment.PurchaseDate == PurchaseDate);
            }

            if (WarrantyExpirationDate is not null)
            {
                query = query.And(equipment => equipment.WarrantyExpirationDate == WarrantyExpirationDate);
            }

            if (GymId is not null)
            {
                var gymId = new GymId(GymId.Value);
                query = query.And(equipment => equipment.GymId == gymId);
            }

            if (Name is not null)
            {
                query = query.And(equipment => equipment.Name.ToLower().Contains(Name.ToLower()));
            }

            return query;
        }
    }
}
