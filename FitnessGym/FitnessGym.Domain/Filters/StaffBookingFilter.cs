using FitnessGym.Domain.Entities.Enums;
using FitnessGym.Domain.Entities.Members;
using System.Linq.Expressions;
using Utils.Shared;

namespace FitnessGym.Domain.Filters
{
    public class StaffBookingFilter
    {
        public Guid StaffId { get; set; }
        public Guid? MemberId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public BookingRequestStatus? RequestStatus { get; set; }

        public Expression<Func<StaffBooking, bool>> GetQuery()
        {
            Expression<Func<StaffBooking, bool>> query = booking => booking.StaffId == StaffId;

            if (MemberId is not null)
            {
                query = query.And(booking => booking.MemberId == MemberId);
            }

            if (StartDate is not null)
            {
                query = query.And(booking => booking.SessionStart.Date >= StartDate.Value.ToDateTime(new TimeOnly()).ToUniversalTime().Date);
            }

            if (EndDate is not null)
            {
                query = query.And(booking => booking.SessionStart.Date <= EndDate.Value.ToDateTime(new TimeOnly()).ToUniversalTime().Date);
            }

            if (RequestStatus is not null)
            {
                query = query.And(booking => booking.RequestStatus == RequestStatus.Value);
            }

            return query;
        }
    }
}
